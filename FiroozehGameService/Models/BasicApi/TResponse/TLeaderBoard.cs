using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi.TResponse
{
    [Serializable]
    internal class TLeaderBoard
    {
        [JsonProperty("leaderboard")] public List<LeaderBoard> LeaderBoards;
        [JsonProperty("status")] public bool Status;
    }
}