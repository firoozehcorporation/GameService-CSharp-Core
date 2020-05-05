using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.Internal
{
    [Serializable]
    internal class ImageUploadResult
    {
        [JsonProperty("msg")] public string Message;
        [JsonProperty("status")] public bool Status;
        [JsonProperty("url")] public string Url;
    }
}