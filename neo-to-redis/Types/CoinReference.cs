using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace neo_to_redis
{
    [Serializable]
    public class CoinReference
    {
        [JsonProperty("txid")]
        public string PrevHash;

        [JsonProperty("vout")]
        public int PrevIndex;

        [JsonProperty("id")]
        public string Id
        {
            get { return $"{PrevHash}_{PrevIndex}"; }
        }
    }
}
