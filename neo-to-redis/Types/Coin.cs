using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace neo_to_redis
{
    [Serializable]
    public class Coin : TransactionOutput
    {
        [JsonProperty("id")]
        public string Id
        {
            get { return $"{TxId}_{Index}"; }
        }

        [JsonProperty("txid")]
        public string TxId;

        [JsonProperty("assetname")]
        public string AssetName;

        [JsonProperty("precision")]
        public byte AssetPrecision;

        [JsonProperty("tracedhash")]
        public string TracedHash;

        [JsonProperty("coinstate")]
        public CoinState CoinState;
    }
}
