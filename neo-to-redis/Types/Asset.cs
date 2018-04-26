using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace neo_to_redis
{
    public class Asset
    {
        [JsonProperty("hash")]
        public string Id;

        [JsonProperty("type")]
        public AssetType AssetType;

#if !DEBUG //Chinese characters are having an issue in the VS Debugger?
        [JsonProperty("name")]
        public string Name;
#endif

        [JsonProperty("amount")]
        public double Amount;

        [JsonProperty("precision")]
        public byte Precision;

        [JsonProperty("owner")]
        public string Owner;

        [JsonProperty("admin")]
        public string Admin;

        [JsonProperty("transactions")]
        public List<Transaction> Transactions = new List<Transaction>();
    }
}
