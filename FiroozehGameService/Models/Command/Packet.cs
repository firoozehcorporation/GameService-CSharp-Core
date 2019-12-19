using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.Command
{
    [Serializable]
    internal class Packet
    {
        
        [JsonProperty("0")]
        public string Token { get; set; }
        
        [JsonProperty("1")]
        public int Action { get; set; }
                
        [JsonProperty("2")]
        public string Data { get; set; }
        
        [JsonProperty("3")]
        public string Message { get; set; }

        public Packet(string token, int action, string data = null, string message = null)
        {
            Token = token;
            Action = action;
            Data = data;
            Message = message;
        }

        public override string ToString () {
            return "Packet{" +
                   "Hash='" + Token + '\'' +
                   ", Action=" + Action +
                   ", Data='" + Data + '\'' +
                   ", Message='" + Message + '\'' +
                   '}';
        }
    }
}