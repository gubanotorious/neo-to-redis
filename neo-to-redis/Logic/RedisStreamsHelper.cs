using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace neo_to_redis
{
    public class RedisStreamsHelper
    {
        private IDatabase _redisDb;

        public RedisStreamsHelper(ConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }

        /// <summary>
        /// Appends an element to the specified stream
        /// </summary>
        /// <param name="streamName">Name of the stream</param>
        /// <param name="id">Explicit identifier and instance number for the entry in the stream (ie:1523598632996-0) </param>
        /// <param name="key">The key for the entry</param>
        /// <param name="value">The value for the entry</param>
        /// <returns>The identifier of the element that was appended to the stream</returns>
        public RedisResult XAdd(RedisValue streamName, RedisValue? id, RedisValue key, RedisValue value)
        {
            if (!id.HasValue)
                id = "*"; //Auto assign id

            return _redisDb.Execute("XADD", streamName, id.Value, key, value);
        }
    }
}
