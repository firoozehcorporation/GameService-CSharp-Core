using System;
using FiroozehGameService.Models.GSLive;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi.TResponse
{
    [Serializable]
    internal class TMember
    {
        [JsonProperty("status")] public bool Status { set; get; }

        [JsonProperty("data")] public Member Member { set; get; }
    }
}