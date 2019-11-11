using System.Collections.Generic;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi.TResponse
{
    internal class TSubmitScore
    {
        [JsonProperty("status")]
        public bool Status { set; get; }
        
        [JsonProperty("leaderboard")]
        public SubmitScoreResponse SubmitScoreResponse { set; get; }
    }
}