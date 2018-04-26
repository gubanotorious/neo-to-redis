using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace neo_to_redis
{
    [Serializable]
    public class Block
    {
        [JsonProperty("hash")]
        public string Hash;

        [JsonProperty("size")]
        public int Size;

        [JsonProperty("version")]
        public byte Version;

        [JsonProperty("previousblockhash")]
        public string PreviousBlockHash;

        [JsonProperty("merkleroot")]
        public string MerkleRoot;

        [JsonProperty("time")]
        public int Timestamp;

        [JsonProperty("index")]
        public int Index;

        [JsonProperty("nonce")]
        public string ConsensusData;

        [JsonProperty("nextconsensus")]
        public string NextConsensus;

        [JsonProperty("nextblockhash")]
        public string NextBlockHash;

        [JsonProperty("tx")]
        public List<Transaction> Transactions;

        [JsonProperty("script")]
        public Witness Script;

        [JsonProperty("confirmations")]
        public int Confirmations;

        [JsonProperty("txcount")]
        public int TxCount
        {
            get
            {
                return Transactions.Count;
            }
        }

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

        public Block()
        {
            Transactions = new List<Transaction>();
        }
    }
}
