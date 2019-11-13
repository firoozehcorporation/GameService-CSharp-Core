using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.RT
{
    [Serializable]
    internal class StatusPayload
    {
      
        [JsonProperty("1")]
        public bool Status { get; set; }
        
        [JsonProperty("2")]
        public string Message { get; set; }
     
       
        public override string ToString () {
            return "StatusPayload{" +
                   "Status=" + Status +
                   ", SendPublicMessage='" + Message + '\'' +
                   '}';
        }
    }
}