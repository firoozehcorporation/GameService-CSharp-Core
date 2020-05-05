using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.RT
{
    [Serializable]
    internal class PingPongPayload : Payload
    {
        [JsonProperty("1")] public string RoomId;
        [JsonProperty("2")] public string UserHash;


        public PingPongPayload(string roomId, string userHash)
        {
            RoomId = roomId;
            UserHash = userHash;
        }


        public override string ToString()
        {
            return "PingPongPayload{" +
                   "RoomID='" + RoomId + '\'' +
                   ", UserHash='" + UserHash + '\'' +
                   '}';
        }
    }
}