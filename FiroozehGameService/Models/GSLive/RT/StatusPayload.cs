using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.RT
{
    [Serializable]
    internal class StatusPayload : Payload
    {
        [JsonProperty("2")] public string Message;

        [JsonProperty("1")] public bool Status;

        public override string ToString()
        {
            return "StatusPayload{" +
                   "Status=" + Status +
                   ", SendPublicMessage='" + Message + '\'' +
                   '}';
        }
    }
}