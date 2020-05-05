using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.Command
{
    [Serializable]
    internal class AuthPayload : Payload
    {
        [JsonProperty("0")] public string GameId;
        [JsonProperty("1")] public string Token;

        public AuthPayload(string gameId, string token)
        {
            GameId = gameId;
            Token = token;
        }


        public override string ToString()
        {
            return "AuthPayload{" +
                   "GameID='" + GameId + '\'' +
                   ", Hash='" + Token + '\'' +
                   '}';
        }
    }
}