using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.RT
{
    [Serializable]
    internal class DataPayload : Payload
    {
       
        [JsonProperty("1")]
        public string SenderId { get; set; }
        
        [JsonProperty("2")]
        public string ReceiverId { get; set; }
        
        [JsonProperty("3")]
        public string Payload { get; set; }
        
        
        public DataPayload (string senderId = null,string receiverId = null, string payload = null)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            Payload = payload;
        }

        public DataPayload()
        {
        }


        public override string ToString () {
            return "DataPayload{" +
                   ", ReceiverID='" + ReceiverId + '\'' +
                   ", Payload='" + Payload + '\'' +
                   '}';
        }
    }
}