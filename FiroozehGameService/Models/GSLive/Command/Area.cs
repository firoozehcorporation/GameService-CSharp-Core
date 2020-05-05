using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.Command
{
    [Serializable]
    internal class Area
    {
        [JsonProperty("0")] public string Ip;
        [JsonProperty("2")] public int Port;
        [JsonProperty("1")] public string Protocol;

        public override string ToString()
        {
            return "Area{" +
                   "EndPoint='" + Ip + '\'' +
                   ", Protocol='" + Protocol + '\'' +
                   ", Port='" + Port + '\'' +
                   '}';
        }
    }
}