using System;
using FiroozehGameService.Models.GSLive;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.Command
{
    [Serializable]
    internal class AuthPayload : Payload
    {
        [JsonProperty("0")]
        public string GameId { get; set; }
        
        [JsonProperty("1")]
        public string Token { get; set; }
        
        
        public AuthPayload (string gameId, string token) {
            GameId = gameId;
            Token = token;
        }

        
        public override string ToString () {
            return "AuthPayload{" +
                   "GameID='" + GameId + '\'' +
                   ", Token='" + Token + '\'' +
                   '}';
        }
    }
}