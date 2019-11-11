using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi.TResponse
{
    [Serializable]
    internal class TUser
    {
        [JsonProperty("status")]
        public bool Status { set; get; }
        
        [JsonProperty("data")]
        public User User { set; get; }
    }
}