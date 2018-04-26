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

        //Quick in memory storage for now
        private static List<Block> _blocks = new List<Block>();

        static void Main(string[] args)
        {
            _redis = new RedisDbHelper(ConnectionMultiplexer.Connect(_redisConn));
            _neo = new NeoCliHelper(_neoConn);

            GetBlocks(0);
        }

        static void GetBlocks(int start)
        {
            var blockCount = _neo.GetBlockCount();
            for(int i = start; i < blockCount-1; i++)
            {
                try
                {
                    Console.Write("Block [" + i + "/" + blockCount + "]:");
                    var block = _neo.GetBlock(i);
                    Console.WriteLine("Received: " + block.Hash);
                    _blocks.Add(block);                  
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Failed: " + ex.Message);
                    //Bad block... fail to serialize?
                }
            }
        }
    }
}
