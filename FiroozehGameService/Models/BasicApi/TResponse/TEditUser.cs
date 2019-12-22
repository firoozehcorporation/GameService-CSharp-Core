using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi.TResponse
{
    [Serializable]
    internal class TEditUser
    {
        [JsonProperty("status")]
        public bool Status { set; get; }
        
        [JsonProperty("user")]
        public User User { set; get; }
    }
}