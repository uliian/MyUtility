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
    /// Snowflake是取毫秒，这个实现是取秒，ConcurrentDic可以将秒-序列进行绑定，并且缓存一段时间，之后再清除。
    /// 这个比较龊的实现无意间带来一个极其有用的好处：缓解时钟回拨带来的ID冲突
    /// </summary>
    public class EasyGenerator : IIdGenerator
    {
        private DateTime _startTime = new DateTime(2000, 1, 1);
        private readonly long _nodeId;
        private readonly long _dicCount;
        private ConcurrentDictionary<long, IdSeed> _idDic;
        
        public EasyGenerator(short nodeId,int dicCount = 60)
        {
            this._nodeId = nodeId;
            this._dicCount = dicCount;
            this._idDic = new ConcurrentDictionary<long, IdSeed>();
        }

        public IdResult GetIdResult()
        {
            do
            {
                //lock (this)
                //{
                //    var secons = (DateTime.Now - _startTime).TotalSeconds;
                //    long nowTimeStamp = (long)secons;
                //    this._sequence++;
                //    if (this._sequence  < 1048574)
                //    {
                //        var idresult = new IdResult()
                //        {
                //            Timestamp = nowTimeStamp,
                //            NodeId = _nodeId,
                //            Sequence = _sequence
                //        };
                //        return idresult;
                //    }
                //    Thread.Sleep(100);
                //}
                var secons = (DateTime.Now - _startTime).TotalSeconds;
                long nowTimeStamp = (long)secons;

                var seed = this._idDic.GetOrAdd(nowTimeStamp, new IdSeed());

                var sequence = Interlocked.Increment(ref seed.Seed);

                //清理过期key 
                if (sequence == 1 && this._idDic.Count > 1)
                {
                    var keys = this._idDic.Keys.ToList();
                    var orderdKeys = keys.OrderByDescending(x => x);
                    int i = 0;
                    foreach (var timestamp in orderdKeys)
                    {
                        // 存60s的seed，防止时钟回拨
                        if (i < this._dicCount)
                        {
                            continue;
                        }
                        this._idDic.TryRemove(timestamp, out IdSeed _);
                    }
                }

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
