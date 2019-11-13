using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.TB
{
    [Serializable]
    internal class DataPayload : Payload
    {
        [JsonProperty("0")]
        public int Action { get; set; }
        
        [JsonProperty("1")]
        public string Id { get; set; }
        
        [JsonProperty("2")]
        public string Data { get; set; }
        
        [JsonProperty("3")]
        public string NextId { get; set; }
        
        [JsonProperty("4")]
        public Dictionary<string,Outcome> Outcomes { get; set; }
        
        [JsonProperty("5")]
        public string IsPrivate { get; set; }
        
    
        
        public override string ToString () {
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