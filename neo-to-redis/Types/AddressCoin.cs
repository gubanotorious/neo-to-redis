﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace neo_to_redis
{
    [Serializable]
    public class AddressCoin
    {
        [BinaryProperty(1)]
        [JsonProperty("id")]
        public string Id;

        [BinaryProperty(2)]
        [JsonProperty("assetid")]
        public string AssetId;

        [BinaryProperty(3)]
        [JsonProperty("coinstate")]
        public CoinState CoinState;
    }
}
