using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.RT
{
    [Serializable]
    internal class DataPayload : Payload
    {
        [JsonProperty("1")]
        public int Action { get; set; }
        
        [JsonProperty("2")]
        public string RoomId { get; set; }
        
        [JsonProperty("3")]
        public string UserHash { get; set; }
        
        [JsonProperty("4")]
        public string ReceiverId { get; set; }
        
        [JsonProperty("5")]
        public string Payload { get; set; }
        
        
        public DataPayload (int action, string roomId, string userHash, string receiverId = null, string payload = null) {
            Action = action;
            RoomId = roomId;
            UserHash = userHash;
            ReceiverId = receiverId;
            Payload = payload;
        }

        
        public override string ToString () {
            return "DataPayload{" +
                   "Action=" + Action +
                   ", RoomID='" + RoomId + '\'' +
                   ", UserHash='" + UserHash + '\'' +
                   ", ReceiverID='" + ReceiverId + '\'' +
                   ", Payload='" + Payload + '\'' +
                   '}';
        }
    }
}