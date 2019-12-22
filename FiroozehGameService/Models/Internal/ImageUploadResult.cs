using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.Internal
{
    [Serializable]
    internal class ImageUploadResult
    {
        [JsonProperty("status")]
        public bool Status { internal get; set; }
        
        [JsonProperty("url")]
        public string Url { internal get; set; }
        
        [JsonProperty("msg")]
        public string Message { internal get; set; }
    }
}