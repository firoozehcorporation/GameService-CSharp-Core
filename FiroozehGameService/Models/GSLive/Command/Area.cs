using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.Command
{
    [Serializable]
    internal class Area
    {
        [JsonProperty("0")] public string Ip;
        [JsonProperty("1")] public string Protocol;
        [JsonProperty("2")] public int Port;
        [JsonProperty("3")] public string ConnectToken;
        [JsonProperty("4")] public string Cert;

        public override string ToString()
        {
            return "Area{" +
                   "EndPoint='" + Ip + '\'' +
                   ", Protocol='" + Protocol + '\'' +
                   ", Port='" + Port + '\'' +
                   ", ConnectToken='" + ConnectToken + '\'' +
                   ", Cert='" + Cert + '\'' +
                   '}';
        }
    }
}