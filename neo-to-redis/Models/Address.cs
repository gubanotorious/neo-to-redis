using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace neo_to_redis
{
    [Serializable]
    public class Address
    {
        [BinaryProperty(1)]
        [JsonProperty("id")]
        public string Id;

        [BinaryProperty(2)]
        [JsonProperty("transactions")]
        public List<Transaction> Transactions;

        [BinaryProperty(3)]
        [JsonProperty("coins")]
        public List<AddressCoin> Coins;

        [BinaryProperty(4)]
        [JsonProperty("firsttimestamp")]
        public int FirstTimeStamp;

        [BinaryProperty(5)]
        [JsonProperty("lasttimestamp")]
        public int LastTimeStamp;
    }
}
