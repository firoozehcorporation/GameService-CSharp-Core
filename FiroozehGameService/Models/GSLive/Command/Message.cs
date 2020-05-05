using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.Command
{
    [Serializable]
    internal class Message
    {
        [JsonProperty("3")] public string Data;
        [JsonProperty("0")] public bool IsPrivate;
        [JsonProperty("1")] public string ReceiverId;
        [JsonProperty("2")] public string SenderId;
        [JsonProperty("4")] public long Time;

        public Message(bool isPrivate, string receiverId, string sender, string data)
        {
            IsPrivate = isPrivate;
            ReceiverId = receiverId;
            SenderId = sender;
            Data = data;
        }


        public override string ToString()
        {
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