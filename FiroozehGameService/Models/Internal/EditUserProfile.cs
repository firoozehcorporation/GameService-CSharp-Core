using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.Internal
{
    [Serializable]
    internal class EditUserProfile
    {
        [JsonProperty("name")]
        public string NickName { internal get; set; }
                       
        [JsonProperty("auto_add")]
        public bool AllowAutoAddToGame { internal get; set; }
        
        [JsonProperty("public_last_activity")]
        public bool ShowPublicActivity { internal get; set; }
        
        [JsonProperty("group_activity")]
        public bool ShowGroupActivity { internal get; set; }
    }
}