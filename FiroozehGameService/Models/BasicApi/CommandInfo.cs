using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi
{
    [Serializable]
    internal class CommandInfo
    {
        [JsonProperty("k")] public string Cert;
        [JsonProperty("ip")] public string Ip;
        [JsonProperty("port")] public int Port;


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