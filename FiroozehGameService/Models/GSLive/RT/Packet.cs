using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.RT
{
    [Serializable]
    internal class Packet : APacket
    {
        [JsonProperty("1")]
        public int Action { get; set; }
                
        [JsonProperty("2")]
        public string Payload { get; set; }
        
        [JsonProperty("3")]
        public string Hash { get; set; }

        
        public Packet(string hash, int action, string payload = null)
        {
            Hash = hash;
            Action = action;
            Payload = payload;
        }

        public override string ToString () {
            return "Packet{" +
                   "Hash='" + Hash + '\'' +
                   ", Action=" + Action +
                   '}';
        }
    }
}