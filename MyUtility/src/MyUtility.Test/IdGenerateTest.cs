using System;
using System.Collections.Concurrent;
using System.Linq;
using MyUtility.Commons.IdGenerate;
using Xunit;

namespace MyUtility.Test
{
    public class IdGenerateTest
    {
        [Fact]
        public void Generate_test_success()
        {
            for (int index = 0; index < 10; index++)
            {
                var g = new EasyGenerator(10);

                var queue1 = new ConcurrentQueue<long>();
                Enumerable.Range(0, 5097159).AsParallel().WithDegreeOfParallelism(100).ForAll(x =>
                {
                    var idg = g.GetIdResult();
                    var newId = idg.GenerateId();
                    queue1.Enqueue(newId);
                });
                Assert.Equal(queue1.Count, queue1.ToDictionary(x => x, x => x).Count);
            }
        }


        [Fact]
        public void Generate_grows_success()
        {
            var g = new EasyGenerator(10);
            IdResult oldIdResult = new IdResult();
            for (int i = 0; i < 5000000; i++)
            {
                IdResult newIdResult = g.GetIdResult();
                Assert.True(newIdResult.GenerateId() > oldIdResult.GenerateId(), $"{newIdResult}>{oldIdResult}???");
                oldIdResult = newIdResult;
            };
        }
    }
}
