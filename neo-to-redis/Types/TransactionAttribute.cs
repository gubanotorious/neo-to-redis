using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace neo_to_redis
{
    [Serializable]
    public class TransactionAttribute
    {
        [JsonProperty("usage")]
        public TransactionAttributeUsage Usage;

        [JsonProperty("data")]
        public string Data;
    }
}
