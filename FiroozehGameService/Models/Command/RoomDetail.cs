using System;
using FiroozehGameService.Models.BasicApi;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.Command
{
    [Serializable]
    internal class RoomDetail
    {
        [JsonProperty("0")]
        public string Id { get; set; }
        
        [JsonProperty("1")]
        public string User { get; set; }
        
        [JsonProperty("2")]
        public string Invite { get; set; }
        
        [JsonProperty("3")]
        public string Name { get; set; }
        
        [JsonProperty("4")]
        public int Type { get; set; }
        
        [JsonProperty("5")]
        public int Min { get; set; }
        
        [JsonProperty("6")]
        public int Max { get; set; }
        
        [JsonProperty("7")]
        public int GsLiveType { get; set; }
        
        [JsonProperty("8")]
        public string Role { get; set; }
        
        [JsonProperty("9")]
        public bool IsPrivate { get; set; }
        
        public override string ToString () {
            return "RoomDetail{" +
                   "ID='" + Id + '\'' +
                   ", user='" + User + '\'' +
                   ", invite='" + Invite + '\'' +
                   ", name='" + Name + '\'' +
                   ", Type=" + Type +
                   ", Min=" + Min +
                   ", Max=" + Max +
                   ", Role='" + Role + '\'' +
                   ", Private=" + IsPrivate +
                   ", GSLiveType=" + GsLiveType +
                   '}';
        }
    }
}