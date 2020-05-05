using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi.TResponse
{
    [Serializable]
    internal class TSave
    {
        [JsonProperty("game")] public string Game;
        [JsonProperty("new")] public SaveDetails SaveDetails;
        [JsonProperty("status")] public bool Status;
    }
}