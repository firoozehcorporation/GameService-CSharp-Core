using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi.TResponse
{
    [Serializable]
    internal class TLeaderBoard
    {
        [JsonProperty("status")]
        public bool Status { set; get; }
        
        [JsonProperty("leaderboard")]
        public List<LeaderBoard> LeaderBoards { set; get; }
    }
}