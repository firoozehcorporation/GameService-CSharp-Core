using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.RT
{
    [Serializable]
    internal class AuthPayload : Payload
    {
        [JsonProperty("1")]
        public string RoomId { get; set; }
        
        [JsonProperty("2")]
        public string Token { get; set; }
        
        
        public AuthPayload (string roomId, string token) {
            RoomId = roomId;
            Token = token;
        }

        
        public override string ToString () {
            return "AuthPayload{" +
                   "RoomID='" + RoomId + '\'' +
                   ", Hash='" + Token + '\'' +
                   '}';
        }
    }
}