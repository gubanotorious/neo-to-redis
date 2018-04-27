using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace neo_to_redis
{
    class Program
    {
        private static string _redisConn = "localhost";
        private static string _neoConn = "http://localhost:20332"; //Testnet settings

        private static NeoCliHelper _neo;
        private static RedisDbHelper _redis;
        private static RedisStreamsHelper _redisStream;

        static void Main(string[] args)
        {
            var redisConnectionMultiplexer = ConnectionMultiplexer.Connect(_redisConn);

            _redis = new RedisDbHelper(redisConnectionMultiplexer);
            _redisStream = new RedisStreamsHelper(redisConnectionMultiplexer);
            _neo = new NeoCliHelper(_neoConn);

            CopyBlocks(0);
        }

        static void CopyBlocks(int start)
        {
            var blockCount = _neo.GetBlockCount();
            for (int i = start; i < blockCount - 1; i++)
            {
                try
                {
                    Console.WriteLine("Block [" + i + "/" + blockCount + "]:");
                    //Get the json block
                    var jsonBlock = _neo.GetBlock(i);
                    Console.WriteLine("-Hash:" + jsonBlock.Hash);
                    //Get the raw block
                    var rawBlock = _neo.GetRawBlock(i);
                    Console.WriteLine("-Raw Length:" + rawBlock.Length);

                    //Write the json block to the Db
                    _redis.Set(jsonBlock.Hash + "-JSON", JsonConvert.SerializeObject(jsonBlock));
                    Console.WriteLine("-DB Key(json): " + jsonBlock.Hash + "-JSON");
                    //Write the raw block to the Db
                    _redis.Set(jsonBlock.Hash + "-RAW", rawBlock);
                    Console.WriteLine("-DB Key(raw): " + jsonBlock.Hash + "-RAW");
                    //Write the raw block to the Stream
                    var result = _redisStream.XAdd("NeoTestnet", null, jsonBlock.Hash, rawBlock);
                    Console.WriteLine("-STREAM EntryId (auto gen): " + result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed: " + ex.Message);
                }
            }
        }
    }
}
