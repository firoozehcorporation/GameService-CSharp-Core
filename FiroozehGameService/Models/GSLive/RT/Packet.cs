using System;
using FiroozehGameService.Models.Enums;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.RT
{
    [Serializable]
    internal class Packet : APacket
    {
        [JsonProperty("1")] public int Action;
        [JsonProperty("2")] public string Payload;
        [JsonProperty("3")] public string Hash;
        [JsonProperty("4")] public GProtocolSendType SendType;

        
        public Packet(string hash, int action,GProtocolSendType sendType = GProtocolSendType.UnReliable,string payload = null)
        {
            Hash = hash;
            Action = action;
            Payload = payload;
            SendType = sendType;
        }

        public override string ToString()
        {
            return "Packet{" +
                   "Hash='" + Hash + '\'' +
                   ", Action=" + Action +
                   '}';
        }
    }
}