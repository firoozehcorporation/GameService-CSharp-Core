using System;
using FiroozehGameService.Models.BasicApi;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.Command
{
    [Serializable]
    internal class Invite
    {
        [JsonProperty("inviter")]
        public bool Inviter { get; set; }
        
        [JsonProperty("invited")]
        public string Invited { get; set; }
        
        [JsonProperty("room")]
        public string RoomId { get; set; }
        
        [JsonProperty("status")]
        public int Status { get; set; }
        
        [JsonProperty("game")]
        public string GameId { get; set; }
        
        
        public override string ToString () {
            return "Invite{" +
                   "inviter='" + Inviter + '\'' +
                   ", invited='" + Invited + '\'' +
                   ", room=" + RoomId +
                   ", status=" + Status +
                   ", GameID='" + GameId + '\'' +
                   '}';
        }
    }
}