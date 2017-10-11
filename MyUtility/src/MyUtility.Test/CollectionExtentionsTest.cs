using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyUtility.CollectionExtentions;
using Xunit;

namespace MyUtility.Test
{
    public class CollectionExtentionsTest
    {
        [Fact]
        public void CalcluteChangeTest_Success()
        {
            var source = new[] { 1, 2, 3 };
            var newSource = new[] { 2, 3, 4 };
            var (adds,subs) = source.CalcluteChange(newSource);
            Assert.Equal(adds.First(),4);
            Assert.Equal(subs.First(),1);
            Assert.Equal(adds.Count(),subs.Count());
        }
    }
}
