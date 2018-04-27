using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace neo_to_redis
{
    [Serializable]
    public class Witness
    {
        [BinaryProperty(1)]
        [JsonProperty("invocation")]
        public string InvocationScript;

        [BinaryProperty(2)]
        [JsonProperty("verification")]
        public string VerificationScript;
    }
}
