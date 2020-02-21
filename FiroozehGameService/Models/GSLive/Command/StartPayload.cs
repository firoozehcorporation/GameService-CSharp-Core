using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.Command
{
    [Serializable]
    internal class StartPayload
    {
        [JsonProperty("0")]
        public Room Room { get; set; }
        
        [JsonProperty("1")]
        public string MemberId { get; set; }
        
        [JsonProperty("2")]
        public Area Area { get; set; }
        
                
        public override string ToString () {
            return "StartPayload{" +
                   "room=" + Room +
                   ", MemberID='" + MemberId + '\'' +
                   ", area=" + Area +
                   '}';
        }
    }
}