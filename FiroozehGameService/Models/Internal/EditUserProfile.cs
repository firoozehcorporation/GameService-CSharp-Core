using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.Internal
{
    [Serializable]
    internal class EditUserProfile
    {
        [JsonProperty("name")] public string NickName;
        [JsonProperty("mobile")] public string PhoneNumber;
    }
}