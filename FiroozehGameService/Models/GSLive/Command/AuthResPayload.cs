using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.Command
{
    [Serializable]
    internal class AuthResPayload
    {
        [JsonProperty("0")] public string Token;

        public override string ToString()
        {
            return "AuthResPayload{" +
                   "Hash='" + Token + '\'' +
                   '}';
        }
    }
}