using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace neo_to_redis
{
    [Serializable]
    public class AddressCoin
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("assetid")]
        public string AssetId;

        [JsonProperty("coinstate")]
        public CoinState CoinState;
    }
}
