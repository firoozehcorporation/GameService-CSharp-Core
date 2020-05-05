using System;
using FiroozehGameService.Models.GSLive;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi.TResponse
{
    [Serializable]
    internal class TMember
    {
        [JsonProperty("data")] public Member Member;
        [JsonProperty("status")] public bool Status;
    }
}