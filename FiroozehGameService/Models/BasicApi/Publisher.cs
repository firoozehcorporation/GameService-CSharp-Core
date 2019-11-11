using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi
{
    [Serializable]
    public class Publisher
    {
       [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("logoURL")]
        public string LogoUrl { get; set; }
    }
}