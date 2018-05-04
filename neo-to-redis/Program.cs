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
            //var redisConnectionMultiplexer = ConnectionMultiplexer.Connect(_redisConn);

            //_redis = new RedisDbHelper(redisConnectionMultiplexer);
            //_redisStream = new RedisStreamsHelper(redisConnectionMultiplexer);
            _neo = new NeoCliHelper(_neoConn);

            //CopyBlocks(0);

            var bytesBlock = _neo.GetBlock(50, false);
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
                var bytesRaw = (byte[])_neo.GetRawBlock(index, false);

                //Get the converted json block (test our json serializers)
                var jsonBlock = _neo.GetBlock(index, true);
                Console.WriteLine("-Retrieved Hash:" + jsonBlock.Hash + ", Raw Length: " + bytesRaw.Length);

                //Write the raw block to the Db
                _redis.Set(jsonBlock.Hash + "-RAW-R", bytesRaw);
                Console.WriteLine("-Wrote Raw Bytes to DB - Key: " + jsonBlock.Hash + "-RAW");

                //TODO: Get this working to test the byte[] -> object -> byte[] serialization
                //Write the converted bytes block to the Db
                //var bytesBlock = _neo.GetBlock(index, false);
                //_redis.Set(jsonBlock.Hash + "-RAW-S", BinarySerializer.Serialize(bytesBlock);
                //Console.WriteLine("-Wrote Converted Bytes to DB - Key: " + jsonBlock.Hash + "-RAW-S");

                //Write the raw block to the Stream
                var result = _redisStream.XAdd("NeoTestnet", null, jsonBlock.Hash, bytesRaw);
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
