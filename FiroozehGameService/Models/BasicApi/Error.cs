using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi
{
    [Serializable]
    internal class Error
    {
        [JsonProperty("status")]
        public bool Status { get; set; }
        
        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}