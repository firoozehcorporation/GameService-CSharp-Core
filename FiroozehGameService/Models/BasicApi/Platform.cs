using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi
{
    [Serializable]
    public class Platform
    {
        [JsonProperty("os")]
        public string Os { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }
    }
}