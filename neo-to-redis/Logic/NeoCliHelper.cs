using NeoSharp.Core.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace neo_to_redis
{
    public class NeoCliHelper
    {
        private string _rpcUrl;

        /// <summary>
        /// Creates an instance of the NeoCliHelper to wrap all interaction with neo-cli
        /// </summary>
        /// <param name="rpcUrl">The neo-cli RPC url to use</param>
        public NeoCliHelper(string rpcUrl)
        {
            _rpcUrl = rpcUrl;
        }


        /// <summary>
        /// Retrieves the total number of blocks
        /// </summary>
        /// <returns>Total number of blocks</returns>
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

        /// <summary>
        /// Retrieves an object that is the raw data of the block received from neo-cli
        /// </summary>
        /// <param name="index">Block index</param>
        /// <param name="verbose">Whether or not to make a verbose request to neo-cli (will return JSON vs binary payload)</param>
        /// <returns>Object (byte[] or string) representing the raw block data received from neo-cli</returns>
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


        /// <summary>
        /// Retrieves a block from neo-cli deserialized from the raw data into the object model
        /// </summary>
        /// <param name="index">Block index</param>
        /// <param name="json">Use json data payload and deserialization instead of binary payload and deserialization</param>
        /// <returns>The deserialized Block object</returns>
        public Block GetBlock(int index, bool json)
        {
            if (json)
            {
                string rawJson = (string)GetRawBlock(index, true);
                return JsonConvert.DeserializeObject<Block>(rawJson);
            }
            else
            {
                //res is the raw bytes
                byte[] rawBytes = (byte[])GetRawBlock(index, false);
                return BinarySerializer.Deserialize<Block>(rawBytes);
            }
        }

        /// <summary>
        /// Retrieves info about an asset from neo-cli
        /// </summary>
        /// <param name="hash">Hash of the asset to retrieve</param>
        /// <returns>The deserialized Asset object</returns>
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

        /// <summary>
        /// Make the call to the neo-cli RPC service for the specified request
        /// </summary>
        /// <param name="url">The URL of the RPC endpoint</param>
        /// <param name="request">The JsonRpcRequest to use in making the request</param>
        /// <returns>Dynamic object representing the json result field</returns>
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
}
