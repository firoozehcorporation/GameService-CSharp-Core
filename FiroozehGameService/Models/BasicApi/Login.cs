using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi
{
    public class Login
    {
        [JsonProperty("status")]
        public bool Status { get; set; }
        
        [JsonProperty("token")]
        public string UserToken { get; set; }
        
        [JsonProperty("game")]
        public string Game { get; set; }
    }
}