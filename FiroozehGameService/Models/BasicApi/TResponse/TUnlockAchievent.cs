using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi.TResponse
{
    [Serializable]
    internal class TUnlockAchievement
    {
        [JsonProperty("new")] public Achievement Achievement;
        [JsonProperty("status")] public bool Status;
    }
}