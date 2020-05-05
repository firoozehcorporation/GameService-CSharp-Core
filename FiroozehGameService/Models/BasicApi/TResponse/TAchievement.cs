using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi.TResponse
{
    [Serializable]
    internal class TAchievement
    {
        [JsonProperty("achievement")] public List<Achievement> Achievements;
        [JsonProperty("status")] public bool Status;
    }
}