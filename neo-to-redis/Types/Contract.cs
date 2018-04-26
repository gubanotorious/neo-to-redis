using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace neo_to_redis
{
    [Serializable]
    public class Contract
    {
        public string Hash
        {
            get
            {
                return Code.Hash;
            }
        }
        public string Script
        {
            get
            {
                return Code.Script;
            }
        }
        public List<string> Parameters
        {
            get
            {
                return Code.Parameters;
            }
        }
        public string ReturnType
        {
            get
            {
                return Code.ReturnType;
            }
        }

        [JsonProperty("code")]
        public Code Code { get; set; }

        [JsonProperty("needstorage")]
        public string NeedStorage;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("version")]
        public string Version;

        [JsonProperty("author")]
        public string Author;

        [JsonProperty("email")]
        public string Email;

        [JsonProperty("description")]
        public string Description;
    }
}
