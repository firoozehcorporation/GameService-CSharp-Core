using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi
{
    [Serializable]
    internal class Error
    {
        [JsonProperty("msg")] public string Message;
        [JsonProperty("status")] public bool Status;
    }
}