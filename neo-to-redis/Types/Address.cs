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
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("transactions")]
        public List<Transaction> Transactions;

        [JsonProperty("coins")]
        public List<AddressCoin> Coins;

        [JsonProperty("firsttimestamp")]
        public int FirstTimeStamp;

        [JsonProperty("lasttimestamp")]
        public int LastTimeStamp;
    }
}
