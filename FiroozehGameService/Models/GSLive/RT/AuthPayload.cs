using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.RT
{
    [Serializable]
    internal class AuthPayload : Payload
    {
        [JsonProperty("1")] public string RoomId;
        [JsonProperty("2")] public string Token;

        public AuthPayload(string roomId, string token)
        {
            RoomId = roomId;
            Token = token;
        }


        public override string ToString()
        {
            return "AuthPayload{" +
                   "RoomID='" + RoomId + '\'' +
                   ", Hash='" + Token + '\'' +
                   '}';
        }
    }
}