using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi
{
    [Serializable]
    internal class CommandInfo
    {
        [JsonProperty("ip")] public string Ip;
        [JsonProperty("port")] public int Port;
        [JsonProperty("k")] public string Cert;
        
        
        public override string ToString()
        {
            return "CommandInfo{" +
                   "ip='" + Ip + '\'' +
                   ", port='" + Port + '\'' +
                   ", cert='" + Cert + '\'' +
                   '}';
        }
        
    }
}