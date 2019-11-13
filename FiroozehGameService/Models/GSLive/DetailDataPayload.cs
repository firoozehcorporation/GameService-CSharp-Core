using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive
{
    [Serializable]
    internal class DetailDataPayload
    {
      
        [JsonProperty("1")]
        public string RoomId { get; set; }
        
        [JsonProperty("2")]
        public string SenderId { get; set; }
                
        [JsonProperty("3")]
        public string Payload { get; set; }
        
        public DetailDataPayload (string roomId, string senderId, string payload) {
            RoomId = roomId;
            SenderId = senderId;
            Payload = payload;
        }

        
        public override string ToString () {
            return "DetailDataPayload{" +
                   "RoomID='" + RoomId + '\'' +
                   ", SenderID='" + SenderId + '\'' +
                   ", Payload='" + Payload + '\'' +
                   '}';
        }
    }
}