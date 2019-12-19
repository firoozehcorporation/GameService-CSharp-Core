using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.TB
{
    [Serializable]
    internal class AuthPayload : Payload
    {
        [JsonProperty("0")]
        public string RoomId { get; set; }
        
        [JsonProperty("1")]
        public string Token { get; set; }
        
        
        public AuthPayload (string roomId, string token) {
            RoomId = roomId;
            Token = token;
        }

        
        public override string ToString () {
            return "AuthPayload{" +
                   "RoomId='" + RoomId + '\'' +
                   ", Hash='" + Token + '\'' +
                   '}';
        }
    }
}