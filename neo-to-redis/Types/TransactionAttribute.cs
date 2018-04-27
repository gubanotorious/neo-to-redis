using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace neo_to_redis
{
    [Serializable]
    public class TransactionAttribute
    {
        [BinaryProperty(1)]
        [JsonProperty("usage")]
        public TransactionAttributeUsage Usage;

        [BinaryProperty(2)]
        [JsonProperty("data")]
        public string Data;
    }
}
