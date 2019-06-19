using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbe.Mahua.Plugins.iChunqiuQQBoot.Beta.Tools
{
    /// <summary>
    /// Redis工具类
    /// </summary>
    class RedisHelper
    {
        public static IDatabase getRedis()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(Constants.redisStr);
            IDatabase db = redis.GetDatabase();
            return db;
        }
    }
}
