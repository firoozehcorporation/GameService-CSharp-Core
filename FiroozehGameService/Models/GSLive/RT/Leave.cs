using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.RT
{
    [Serializable]
    internal class Leave
    {
        [JsonProperty("1")] public string MemberLeaveId;
        [JsonProperty("2")] public string RoomId;

        public Leave(string memberLeaveId, string roomId)
        {
            MemberLeaveId = memberLeaveId;
            RoomId = roomId;
        }


        public override string ToString()
        {
            return "Leave{" +
                   "MemberLeaveID='" + MemberLeaveId + '\'' +
                   ", RoomID='" + RoomId + '\'' +
                   '}';
        }
    }
}