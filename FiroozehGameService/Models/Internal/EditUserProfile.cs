using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.Internal
{
    [Serializable]
    internal class EditUserProfile
    {
        [JsonProperty("auto_add")] public bool AllowAutoAddToGame;
        [JsonProperty("name")] public string NickName;
        [JsonProperty("group_activity")] public bool ShowGroupActivity;
        [JsonProperty("public_last_activity")] public bool ShowPublicActivity;
    }
}