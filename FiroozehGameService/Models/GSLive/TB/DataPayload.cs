using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.TB
{
    [Serializable]
    internal class DataPayload : Payload
    {
        [JsonProperty("0")] public int Action;
        [JsonProperty("2")] public string Data;
        [JsonProperty("1")] public string Id;
        [JsonProperty("5")] public bool IsPrivate;
        [JsonProperty("3")] public string NextId;
        [JsonProperty("4")] public Dictionary<string, Outcome> Outcomes;


        public override string ToString()
        {
            return "DataPayload{" +
                   "Action=" + Action +
                   ", ID='" + Id + '\'' +
                   ", Data='" + Data + '\'' +
                   ", Next='" + NextId + '\'' +
                   ", Outcomes=" + Outcomes +
                   ", Private=" + IsPrivate +
                   '}';
        }
    }
}