using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi.TResponse
{
    [Serializable]
    internal class TUser
    {
        [JsonProperty("status")] public bool Status;
        [JsonProperty("data")] public User User;
    }
}