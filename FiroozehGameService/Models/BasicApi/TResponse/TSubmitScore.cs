using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi.TResponse
{
    [Serializable]
    internal class TSubmitScore
    {
        [JsonProperty("status")] public bool Status;
        [JsonProperty("leaderboard")] public SubmitScoreResponse SubmitScoreResponse;
    }
}