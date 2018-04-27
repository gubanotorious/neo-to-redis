using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace neo_to_redis
{
    [Serializable]
    public class CoinReference
    {
        [BinaryProperty(1)]
        [JsonProperty("txid")]
        public string PrevHash;

        [BinaryProperty(2)]
        [JsonProperty("vout")]
        public int PrevIndex;

        [BinaryProperty(3)]
        [JsonProperty("id")]
        public string Id
        {
            get { return $"{PrevHash}_{PrevIndex}"; }
        }
    }
}
