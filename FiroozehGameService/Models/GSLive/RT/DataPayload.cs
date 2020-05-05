using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.RT
{
    [Serializable]
    internal class DataPayload : Payload
    {
        [JsonProperty("3")] public string Payload;
        [JsonProperty("2")] public string ReceiverId;

        [JsonProperty("1")] public string SenderId;


        public DataPayload(string senderId = null, string receiverId = null, string payload = null)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            Payload = payload;
        }

        public DataPayload()
        {
        }


        public override string ToString()
        {
            return "DataPayload{" +
                   ", ReceiverID='" + ReceiverId + '\'' +
                   ", Payload='" + Payload + '\'' +
                   '}';
        }
    }
}