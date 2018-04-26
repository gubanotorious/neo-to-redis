using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace neo_to_redis
{
    [Serializable]
    public class TransactionOutput
    {
        [JsonProperty("n")]
        public string Index;

        [JsonProperty("asset")]
        public string AssetId;

        [JsonProperty("value")]
        public double Value;

        [JsonProperty("address")]
        public string Address;
    }
}
