using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.Internal
{
    [Serializable]
    internal class Publisher
    {
        [JsonProperty("id")] public string Id;
        [JsonProperty("logoURL")] public string LogoUrl;
        [JsonProperty("name")] public string Name;
    }
}