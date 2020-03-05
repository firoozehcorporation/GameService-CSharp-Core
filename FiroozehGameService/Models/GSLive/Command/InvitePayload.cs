using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.Command
{
    [Serializable]
    internal class InvitePayload
    {
        [JsonProperty("0")]
        public Room Room { get; set; }
        
        [JsonProperty("1")]
        public Member Member { get; set; }
        
        [JsonProperty("2")]
        public Invite Invite { get; set; }
        
        
        public override string ToString () {
            return "InvitePayload{" +
                   "room=" + Room +
                   ", member=" + Member +
                   ", invite=" + Invite +
                   '}';
        }
    }
}