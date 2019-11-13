using System;
using FiroozehGameService.Models.BasicApi;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.Command
{
    [Serializable]
    internal class AutoMatch
    {
       
        [JsonProperty("min")]
        public int Min { get; set; }
        
        [JsonProperty("max")]
        public int Max { get; set; }
                
        [JsonProperty("role")]
        public string Role { get; set; }
        
        [JsonProperty("accept")]
        public bool Accept { get; set; }
        
        public override string ToString () {
            return "AutoMatch{" +
                   "max=" + Max +
                   ", min=" + Min +
                   ", role='" + Role + '\'' +
                   ", accept=" + Accept +
                   '}';
        }
    }
}