using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MyUtility.Commons.IdGenerate
{
    /// <summary>
    /// 这是一个类SnowFlake算法实现，ID可从2000.1.1开始计算秒数，即正常可以用到2099年。
    /// Snowflake是取毫秒，这个实现是取秒，
    /// <del>ConcurrentDic可以将秒-序列进行绑定，并且缓存一段时间，之后再清除。
    /// 这个比较龊的实现无意间带来一个极其有用的好处：缓解时钟回拨带来的ID冲突</del>
    /// 
    /// HOHO~喜大普奔！！实现了CircleArray之后就可以把ConcurrentDictionary干掉了，
    /// 就真的没锁了，而且也没了莫名其妙清除逻辑！散花散花......
    /// </summary>
    public class EasyGenerator : IIdGenerator
    {
        private DateTime _startTime = new DateTime(2000, 1, 1);
        private readonly long _nodeId;
        private readonly CircleArray _circleArray;
        
        public EasyGenerator(short nodeId,int dicCount = 60)
        {
            this._nodeId = nodeId;
            this._circleArray = new CircleArray(dicCount);
            
        }

        public IdResult GetIdResult()
        {
            do
            {
                var secons = (DateTime.Now - _startTime).TotalSeconds;
                long nowTimeStamp = (long)secons;

                var sequence = this._circleArray.GenerateSequence(nowTimeStamp);

                if (sequence < 1048574)
                {
                    var idresult = new IdResult()
                    {
                        Timestamp = nowTimeStamp,
                        NodeId = _nodeId,
                        Sequence = sequence
                    };
                    return idresult;
                }

                Thread.Sleep(100);
            } while (true);
        }

        public long NewId()
        {
            var result = this.GetIdResult();

            return result.GenerateId();
        }
    }
}
