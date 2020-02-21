using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.Command
{
    [Serializable]
    internal class Message
    {
        [JsonProperty("0")]
        public bool IsPrivate { get; set; }
        
        [JsonProperty("1")]
        public string ReceiverId { get; set; }
        
        [JsonProperty("2")]
        public string SenderId { get; set; }
        
        [JsonProperty("3")]
        public string Data { get; set; }
        
        [JsonProperty("4")]
        public long Time { get; set; }
        
        public Message (bool isPrivate, string receiverId, string sender, string data) {
            IsPrivate = isPrivate;
            ReceiverId = receiverId;
            SenderId = sender;
            Data = data;
        }

       

        public override string ToString () {
            return "Message{" +
                   "isPrivate=" + IsPrivate +
                   ", ReceiverID='" + ReceiverId + '\'' +
                   ", Sender='" + SenderId + '\'' +
                   ", Data='" + Data + '\'' +
                   ", Time=" + Time +
                   '}';
        }
    }
}