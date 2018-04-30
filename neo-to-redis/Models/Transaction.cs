using NeoSharp.Core.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace neo_to_redis
{
    [Serializable]
    public class Transaction
    {
        [BinaryProperty(1)]
        [JsonProperty("txid")]
        public string Hash;

        [BinaryProperty(2)]
        [JsonProperty("size")]
        public int Size;

        [BinaryProperty(3)]
        [JsonProperty("type")]
        public TransactionType Type;

        [BinaryProperty(4)]
        [JsonProperty("version")]
        public byte Version;

        [BinaryProperty(5)]
        [JsonProperty("sys_fee")]
        public long SystemFee;

        [BinaryProperty(6)]
        [JsonProperty("net_fee")]
        public long NetworkFee;

        [BinaryProperty(7)]
        [JsonProperty("blockhash")]
        public string BlockHash;

        [BinaryProperty(8)]
        [JsonProperty("blockindex")]
        public int BlockIndex;

        [BinaryProperty(9)]
        [JsonProperty("timestamp")]
        public int Timestamp;

        [BinaryProperty(10)]
        [JsonProperty("attributes")]
        public List<TransactionAttribute> Attributes = new List<TransactionAttribute>();

        [BinaryProperty(11)]
        [JsonProperty("vin")]
        public List<CoinReference> Inputs = new List<CoinReference>();

        [BinaryProperty(12)]
        [JsonProperty("vout")]
        public List<TransactionOutput> Outputs = new List<TransactionOutput>();

        [BinaryProperty(13)]
        [JsonProperty("scripts")]
        public List<Witness> Scripts;

        //Enrollment
        [BinaryProperty(14)]
        [JsonProperty("pubkey")]
        public string PublicKey;

        //Invocation
        [BinaryProperty(15)]
        [JsonProperty("script")]
        public string Script;

        //Invocation
        [BinaryProperty(16)]
        [JsonProperty("gas")]
        public long Gas;

        //Miner
        [BinaryProperty(17)]
        [JsonProperty("nonce")]
        public ulong Nonce;

        //Claim
        [BinaryProperty(18)]
        [JsonProperty("claims")]
        public List<CoinReference> Claims = new List<CoinReference>();

        //Publish
        [BinaryProperty(19)]
        [JsonProperty("contract")]
        public Contract Contract;

        //Register
        [BinaryProperty(20)]
        [JsonProperty("asset")]
        public Asset Asset;
    }
}