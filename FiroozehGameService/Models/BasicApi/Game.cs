using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi
{
    [Serializable]
    internal class Game
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("_id")]
        public string _Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("package")]
        public string Package { get; set; }
        
        [JsonProperty("category")]
        public string Category { get; set; }
        
        [JsonProperty("installed")]
        public int Installed { get; set; }
        
        [JsonProperty("explane")]
        public string Explane { get; set; }
        
        [JsonProperty("created")]
        public int Created { get; set; }
        
        [JsonProperty("link")]
        public string Link { get; set; }
        
        [JsonProperty("status")]
        public int Status { get; set; }
        
        [JsonProperty("logoURL")]
        public string LogoUrl { get; set; }
        
        [JsonProperty("coverURL")]
        public string CoverUrl { get; set; }
        
        [JsonProperty("publisher")]
        public Publisher Publisher { get; set; }
        
        [JsonProperty("pictures")]
        public List<string> Pictures { get; set; }
        
        [JsonProperty("platforms")]
        public List<Platform> Platforms { get; set; }
    }
}