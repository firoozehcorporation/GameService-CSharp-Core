using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.Internal
{
    [Serializable]
    internal class Game
    {
        [JsonProperty("_id")] public string _Id;

        [JsonProperty("category")] public string Category;

        [JsonProperty("coverURL")] public string CoverUrl;

        [JsonProperty("created")] public int Created;

        [JsonProperty("explane")] public string Explane;
        [JsonProperty("id")] public string Id;

        [JsonProperty("installed")] public int Installed;

        [JsonProperty("link")] public string Link;

        [JsonProperty("logoURL")] public string LogoUrl;

        [JsonProperty("name")] public string Name;

        [JsonProperty("package")] public string Package;

        [JsonProperty("pictures")] public List<string> Pictures;

        [JsonProperty("platforms")] public List<Platform> Platforms;

        [JsonProperty("publisher")] public Publisher Publisher;

        [JsonProperty("status")] public int Status;
    }
}