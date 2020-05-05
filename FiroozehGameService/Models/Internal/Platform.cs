using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.Internal
{
    [Serializable]
    internal class Platform
    {
        [JsonProperty("link")] public string Link;
        [JsonProperty("os")] public string Os;
        [JsonProperty("type")] public string Type;
    }
}