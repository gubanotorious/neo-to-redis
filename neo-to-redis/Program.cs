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
                CopyBlock(i, blockCount);
            }
        }

        static void CopyBlock(int index, int totalBlocks)
        {
            try
            {
                Console.WriteLine("Copying Block [" + index + "/" + totalBlocks + "]:");

                //Get the raw neo-cli
                var jsonRaw = (string)_neo.GetRawBlock(index, true);
                var rawBlock = (byte[])_neo.GetRawBlock(index, false);

                //Get the converted json block (test our json serializers)
                var jsonBlock = _neo.GetBlock(index);
                Console.WriteLine("-Retrieved Hash:" + jsonBlock.Hash + ", Raw Length: " + rawBlock.Length);

                //Write the raw block to the Db
                _redis.Set(jsonBlock.Hash + "-RAW", rawBlock);
                Console.WriteLine("-Wrote Raw Bytes to DB - Key: " + jsonBlock.Hash + "-RAW");

                //Write the raw block to the Stream
                var result = _redisStream.XAdd("NeoTestnet", null, jsonBlock.Hash, rawBlock);
                Console.WriteLine("-Wrote Raw Bytes to Stream - EntryId (auto gen): " + result);

                //Write the raw json block to the Db
                _redis.Set(jsonBlock.Hash + "-JSON-R", jsonRaw);
                Console.WriteLine("-Wrote Raw Json to DB - Key: " + jsonBlock.Hash + "-JSON-R");

                //Write the json block to the Db
                _redis.Set(jsonBlock.Hash + "-JSON-S", JsonConvert.SerializeObject(jsonBlock));
                Console.WriteLine("-Wrote Converted Json to DB - Key: " + jsonBlock.Hash + "-JSON-S");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed: " + ex.Message);
            }
        }
    }
}
