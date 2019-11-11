using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi.TResponse
{
    [Serializable]
    internal class TSave
    {
        
        [JsonProperty("status")]
        public bool Status { set; get; }
        
        [JsonProperty("new")]
        public SaveDetails SaveDetails { set; get; }
    }
}