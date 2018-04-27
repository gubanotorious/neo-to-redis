using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace neo_to_redis
{
    public class NeoCliHelper
    {
        private string _rpcUrl;

        public NeoCliHelper(string rpcUrl)
        {
            _rpcUrl = rpcUrl;
        }

        public int GetBlockCount()
        {
            var res = CallService(_rpcUrl, new JsonRpcRequest
            {
                Method = NeoRpcMethod.getblockcount
            });

            if (res != null)
            {
                return int.Parse(res.ToString());
            }
                
            return 0;
        }

        public object GetRawBlock(int index, bool verbose)
        {
            var request = new JsonRpcRequest
            {
                Method = NeoRpcMethod.getblock,
                Parameters = new List<object> { index }
            };
            if (verbose)
                request.Parameters.Add(1); //,1 = verbose

            var res = CallService(_rpcUrl, request);
            if (res != null)
            {          
                if (verbose)
                    return res.ToString().Replace("\r\n", "").Replace(" ", ""); //Sanitize against newline and whitespace
                else
                    return Encoding.ASCII.GetBytes(res.ToString());
            }

            return null;
        }

        public Block GetBlock(int index)
        {
            //Get the raw JSON
            var res = GetRawBlock(index, true);
            return JsonConvert.DeserializeObject<Block>(res.ToString());
        }

        public Asset GetAsset(string hash)
        {
            var res = CallService(_rpcUrl, new JsonRpcRequest
            {
                Method = NeoRpcMethod.getassetstate,
                Parameters = new List<object> { hash }
            });

            if (res != null)
                return JsonConvert.DeserializeObject<Asset>(res.ToString());

            return null;
        }

        private dynamic CallService(string url, JsonRpcRequest request)
        {
            using (var client = new WebClient { Encoding = System.Text.Encoding.UTF8 })
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var serializedRequest = JsonConvert.SerializeObject(request);
                var result = client.UploadString(url, serializedRequest);

                var json = (dynamic)JsonConvert.DeserializeObject(result);
                if (json != null) {
                    if(json.error != null)
                        throw new Exception(json.message);
                    else if(json.result != null)
                        return json.result;
                }
            }

            return null;
        }
    }

    [Serializable]
    public class JsonRpcRequest
    {
        [JsonProperty("jsonrpc")]
        public string JsonRpcVersion = "2.0";

        [JsonProperty("method")]
        public NeoRpcMethod Method = NeoRpcMethod.none;

        [JsonProperty("params")]
        public List<object> Parameters = new List<object>();

        [JsonProperty("id")]
        public int Id = 1;
    }

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
