using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.Command
{
    [Serializable]
    internal class AutoMatch
    {
        [JsonProperty("accept")] public bool Accept;
        [JsonProperty("max")] public int Max;

        [JsonProperty("min")] public int Min;
        [JsonProperty("role")] public string Role;

        public override string ToString()
        {
            return "AutoMatch{" +
                   "max=" + Max +
                   ", min=" + Min +
                   ", role='" + Role + '\'' +
                   ", accept=" + Accept +
                   '}';
        }
    }
}