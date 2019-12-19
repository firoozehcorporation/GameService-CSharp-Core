using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.Command
{
    [Serializable]
    internal class AuthResPayload
    {
        [JsonProperty("0")]
        public string Token { get; set; }
                
        public override string ToString () {
            return "AuthResPayload{" +
                   "Hash='" + Token + '\'' +
                   '}';
        }
    }
}