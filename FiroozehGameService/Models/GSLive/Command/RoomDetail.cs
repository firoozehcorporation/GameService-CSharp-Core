using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.Command
{
    [Serializable]
    internal class RoomDetail
    {
        [JsonProperty("11")] public string Extra;
        [JsonProperty("12")] public string RoomPassword;
        [JsonProperty("7")] public int GsLiveType;
        [JsonProperty("0")] public string Id;
        [JsonProperty("2")] public string Invite;
        [JsonProperty("10")] public bool IsPersist;
        [JsonProperty("9")] public bool IsPrivate;
        [JsonProperty("6")] public int Max;
        [JsonProperty("5")] public int Min;
        [JsonProperty("3")] public string Name;
        [JsonProperty("8")] public string Role;
        [JsonProperty("4")] public int Type;
        [JsonProperty("1")] public string UserOrMemberId;

        public override string ToString()
        {
            return "RoomDetail{" +
                   "ID='" + Id + '\'' +
                   ", user='" + UserOrMemberId + '\'' +
                   ", invite='" + Invite + '\'' +
                   ", name='" + Name + '\'' +
                   ", Extra='" + Extra + '\'' +
                   ", Type=" + Type +
                   ", Min=" + Min +
                   ", Max=" + Max +
                   ", Role='" + Role + '\'' +
                   ", Private=" + IsPrivate +
                   ", Persist=" + IsPersist +
                   ", GSLiveType=" + GsLiveType +
                   '}';
        }
    }
}