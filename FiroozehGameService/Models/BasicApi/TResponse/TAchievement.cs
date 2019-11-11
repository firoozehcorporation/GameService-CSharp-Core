using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi.TResponse
{
    [Serializable]
    internal class TAchievement
    {
        [JsonProperty("status")]
        public bool Status { set; get; }
        
        [JsonProperty("achievement")]
        public List<Achievement> Achievements { set; get; }
    }
}