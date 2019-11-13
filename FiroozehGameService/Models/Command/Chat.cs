using System;
using FiroozehGameService.Models.BasicApi;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.Command
{
    [Serializable]
    internal class Chat
    {
        [JsonProperty("0")]
        public bool IsPrivate { get; set; }
        
        [JsonProperty("1")]
        public string ReceiverId { get; set; }
        
        [JsonProperty("2")]
        public User Sender { get; set; }
        
        [JsonProperty("3")]
        public string Message { get; set; }
        
        [JsonProperty("4")]
        public long SendTime { get; set; }
        
        public override string ToString () {
            return "Chat{" +
                   "IsPrivate=" + IsPrivate +
                   ", ReceiverId='" + ReceiverId + '\'' +
                   ", Sender=" + Sender +
                   ", Message='" + Message + '\'' +
                   ", SendTime=" + SendTime +
                   '}';
        }
    }
}