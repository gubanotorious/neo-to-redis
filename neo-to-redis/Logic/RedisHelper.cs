using System.Collections.Generic;
using StackExchange.Redis;


namespace neo_to_redis
{
    public class RedisDbHelper
    {
        private IDatabase _redisDb;

        /// <summary>
        /// Creates an instance of the RedisDbHelper that will wrap all Redis DB interaction
        /// </summary>
        /// <param name="redis">The redis connection to use</param>
        /// <param name="dbNumber">The database number to use</param>
        public RedisDbHelper(ConnectionMultiplexer redis, int dbNumber = 0)
        {
            _redisDb = redis.GetDatabase(dbNumber);
        }

        /// <summary>
        /// Sets a value in the active Redis DB for the instance of the helper
        /// </summary>
        /// <param name="key">Key to use (byte[] or string)</param>
        /// <param name="val">Value to use (byte[] or string)</param>
        public void Set(RedisKey key, RedisValue val)
        {
            _redisDb.StringSet(key, val);
        }
    }
}
