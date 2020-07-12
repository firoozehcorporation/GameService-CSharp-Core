using System;
using FiroozehGameService.Models.Internal;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi
{
    [Serializable]
    internal class Login
    {
        [JsonProperty("command")] public CommandInfo CommandInfo;
        [JsonProperty("game")] public Game Game;
        [JsonProperty("status")] public bool Status;
        [JsonProperty("token")] public string Token;
    }
}