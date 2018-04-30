using NeoSharp.Core.Serializers;
using Newtonsoft.Json;
using System;

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