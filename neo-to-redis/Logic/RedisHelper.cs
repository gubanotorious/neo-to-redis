using System.Collections.Generic;
using StackExchange.Redis;


namespace neo_to_redis
{
    public class RedisDbHelper
    {
        private IDatabase _redisDb;

        public RedisDbHelper(ConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }

        public void Set(RedisKey key, RedisValue val)
        {
            _redisDb.StringSet(key, val);
        }
    }
}
