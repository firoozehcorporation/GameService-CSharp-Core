using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.Command
{
    [Serializable]
    internal class Packet : APacket
    {
        [JsonProperty("0")] public string Token;
        [JsonProperty("1")] public int Action;
        [JsonProperty("2")] public string Data;
        [JsonProperty("3")] public string Message;
        
        public Packet(string token, int action, string data = null, string message = null)
        {
            Token = token;
            Action = action;
            Data = data;
            Message = message;
        }

        public override string ToString()
        {
            return "Packet{" +
                   "Hash='" + Token + '\'' +
                   ", Action=" + Action +
                   ", Data='" + Data + '\'' +
                   ", Message='" + Message + '\'' +
                   '}';
        }

        internal override byte[] Serialize()
        {
            return ConvertToBytes(JsonConvert.SerializeObject(this));
        }

        internal override void Deserialize(byte[] buffer)
        {
        }

        internal override int BufferSize(short prefixLen)
        {
            return 0;
        }
    }
}