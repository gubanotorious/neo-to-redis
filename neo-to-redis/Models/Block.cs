﻿using NeoSharp.Core.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace neo_to_redis
{
    [Serializable]
    public class Block
    {
        [BinaryProperty(1)]
        [JsonProperty("hash")]
        public string Hash;

        [BinaryProperty(2)]
        [JsonProperty("size")]
        public int Size;

        [BinaryProperty(3)]
        [JsonProperty("version")]
        public byte Version;

        [BinaryProperty(4)]
        [JsonProperty("previousblockhash")]
        public string PreviousBlockHash;

        [BinaryProperty(5)]
        [JsonProperty("merkleroot")]
        public string MerkleRoot;

        [BinaryProperty(6)]
        [JsonProperty("time")]
        public int Timestamp;

        [BinaryProperty(7)]
        [JsonProperty("index")]
        public int Index;

        [BinaryProperty(8)]
        [JsonProperty("nonce")]
        public string ConsensusData;

        [BinaryProperty(9)]
        [JsonProperty("nextconsensus")]
        public string NextConsensus;

        [BinaryProperty(10)]
        [JsonProperty("nextblockhash")]
        public string NextBlockHash;

        [BinaryProperty(11)]
        [JsonProperty("tx")]
        public List<Transaction> Transactions = new List<Transaction>();

        [BinaryProperty(12)]
        [JsonProperty("script")]
        public Witness Script;

        [BinaryProperty(13)]
        [JsonProperty("confirmations")]
        public int Confirmations;

        [BinaryProperty(14)]
        [JsonProperty("txcount")]
        public int TxCount
        {
            get
            {
                return Transactions.Count;
            }
        }

        [BinaryProperty(15)]
        [JsonProperty("txhashes")]
        public List<string> TxHashes
        {
            get
            {
                if(Transactions != null)
                    return Transactions.Select(h => h.BlockHash).ToList();

                return new List<string>();
            }
        }
    }
}