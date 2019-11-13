using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.RT
{
    [Serializable]
    internal class Packet
    {        
        [JsonProperty("1")]
        public int Action { get; set; }
                
        [JsonProperty("2")]
        public string Payload { get; set; }
              
        
        public Packet (int action, string payload) {
            Action = action;
            Payload = payload;
        }
        
        public override string ToString () {
            return "Packet{" +
                   ", Action=" + Action +
                   ", Data='" + Payload + '\'' +
                   '}';
        }
    }
}