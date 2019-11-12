using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi.TResponse
{
    [Serializable]
    internal class TUnlockAchievement
    {
        
        [JsonProperty("status")]
        public bool Status { set; get; }
        
        [JsonProperty("new")]
        public Achievement Achievement { set; get; }
    }
}