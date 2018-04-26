using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace neo_to_redis
{
    [Serializable]
    public class Transaction
    {
        [JsonProperty("txid")]
        public string Hash;

        [JsonProperty("size")]
        public int Size;

        [JsonProperty("type")]
        public TransactionType Type;

        [JsonProperty("version")]
        public byte Version;

        [JsonProperty("sys_fee")]
        public long SystemFee;

        [JsonProperty("net_fee")]
        public long NetworkFee;

        [JsonProperty("blockhash")]
        public string BlockHash;

        [JsonProperty("blockindex")]
        public int BlockIndex;

        [JsonProperty("timestamp")]
        public int Timestamp;

        [JsonProperty("attributes")]
        public List<TransactionAttribute> Attributes = new List<TransactionAttribute>();

        [JsonProperty("vin")]
        public List<CoinReference> Inputs = new List<CoinReference>();

        [JsonProperty("vout")]
        public List<TransactionOutput> Outputs = new List<TransactionOutput>();

        [JsonProperty("scripts")]
        public List<Witness> Scripts;

        //Enrollment
        [JsonProperty("pubkey")]
        public string PublicKey;

        //Invocation
        [JsonProperty("script")]
        public string Script;

        //Invocation
        [JsonProperty("gas")]
        public long Gas;

        //Miner
        [JsonProperty("nonce")]
        public ulong Nonce;

        //Claim
        [JsonProperty("claims")]
        public List<CoinReference> Claims = new List<CoinReference>();

        //Publish
        [JsonProperty("contract")]
        public Contract Contract;

        //Register
        [JsonProperty("asset")]
        public Asset Asset;
    }
}
