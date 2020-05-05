using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive
{
    [Serializable]
    internal class DetailDataPayload
    {
        [JsonProperty("3")] public string Payload;

        [JsonProperty("1")] public string RoomId;
        [JsonProperty("2")] public string SenderId;

        public DetailDataPayload(string roomId, string senderId, string payload)
        {
            RoomId = roomId;
            SenderId = senderId;
            Payload = payload;
        }


        public override string ToString()
        {
            return "DetailDataPayload{" +
                   "RoomID='" + RoomId + '\'' +
                   ", SenderID='" + SenderId + '\'' +
                   ", Payload='" + Payload + '\'' +
                   '}';
        }
    }
}