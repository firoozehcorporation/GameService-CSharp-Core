using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.Command
{
    [Serializable]
    internal class Area
    {
        [JsonProperty("0")]
        public string IP { get; set; }
        
        [JsonProperty("1")]
        public string Protocol { get; set; }
        
        [JsonProperty("2")]
        public int Port { get; set; }
        
        public override string ToString () {
            return "Area{" +
                   "EndPoint='" + IP + '\'' +
                   ", Protocol='" + Protocol + '\'' +
                   ", Port='" + Port + '\'' +
                   '}';
        }
    }
}