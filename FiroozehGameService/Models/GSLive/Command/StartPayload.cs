using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.Command
{
    [Serializable]
    internal class StartPayload
    {
        [JsonProperty("2")] public Area Area;
        [JsonProperty("1")] public string MemberId;

        [JsonProperty("0")] public Room Room;

        public override string ToString()
        {
            return "StartPayload{" +
                   "room=" + Room +
                   ", MemberID='" + MemberId + '\'' +
                   ", area=" + Area +
                   '}';
        }
    }
}