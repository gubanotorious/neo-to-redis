using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace neo_to_redis
{
    [Serializable]
    public class Witness
    {
        [JsonProperty("invocation")]
        public string InvocationScript;

        [JsonProperty("verification")]
        public string VerificationScript;
    }
}
