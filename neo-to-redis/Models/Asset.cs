﻿using NeoSharp.Core.Serializers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace neo_to_redis
{
    public class Asset
    {
        [BinaryProperty(1)]
        [JsonProperty("hash")]
        public string Id;

        [BinaryProperty(2)]
        [JsonProperty("type")]
        public AssetType AssetType;

#if !DEBUG //TODO: Chinese characters are having an issue in the VS Debugger?  We can't serialize Chinese Chars?
        [BinaryProperty(3)]
        [JsonProperty("name")]
        public string Name;
#endif

        [BinaryProperty(4)]
        [JsonProperty("amount")]
        public double Amount;

        [BinaryProperty(5)]
        [JsonProperty("precision")]
        public byte Precision;

        [BinaryProperty(6)]
        [JsonProperty("owner")]
        public string Owner;

        [BinaryProperty(7)]
        [JsonProperty("admin")]
        public string Admin;

        [BinaryProperty(8)]
        [JsonProperty("transactions")]
        public Transaction[] Transactions = new Transaction[0];
    }
}