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

        public void Set(string key, object value)
        {

        }
    }
}
