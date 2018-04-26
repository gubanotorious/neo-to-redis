using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace neo_to_redis
{
    [Serializable]
    public class Code
    {
        [JsonProperty("hash")]
        public string Hash;

        [JsonProperty("script")]
        public string Script;

        [JsonProperty("returntype")]
        public string ReturnType;

        [JsonProperty("parameters")]
        public List<string> Parameters;
    }
}
