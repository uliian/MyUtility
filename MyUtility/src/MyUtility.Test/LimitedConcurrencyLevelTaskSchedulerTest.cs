using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyUtility.Task;
using Xunit;

namespace MyUtility.Test
{
    public class LimitedConcurrencyLevelTaskSchedulerTest
    {
        [Fact]
        public void RunTaskSuccess()
        {
            var factory = new TaskFactory(new LimitedConcurrencyLevelTaskScheduler(1));
            var lst = new List<int>();
            int seed = 0;
            var tsk1 = factory.StartNew(() => lst.Add(seed++));
            var tsk2 = factory.StartNew(() => lst.Add(seed++));
            var tsk3 = factory.StartNew(() => lst.Add(seed++));
            var tsk4 = factory.StartNew(() => lst.Add(seed++));
            var tsk5 = factory.StartNew(() => lst.Add(seed++));
            var tsk6 = factory.StartNew(() => lst.Add(seed++));
            var tsk7 = factory.StartNew(() => lst.Add(seed++));
            var tsk8 = factory.StartNew(() => lst.Add(seed++));
            var tsk9 = factory.StartNew(() => lst.Add(seed++));
            var tsk10 = factory.StartNew(() => lst.Add(seed++));
            System.Threading.Tasks.Task.WaitAll(tsk1, tsk2, tsk3, tsk4, tsk5, tsk6, tsk7, tsk8, tsk9, tsk10);
            for (int i = 0; i < 10; i++)
            {
               Assert.Equal(lst[i],i);
            }
        }
    }
}
