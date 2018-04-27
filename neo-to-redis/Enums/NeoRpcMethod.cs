using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace neo_to_redis
{
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum NeoRpcMethod
    {
        none,
        getassetstate,
        getblockcount,
        getblock
    }
}
