using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi
{
    public class Login
    {
        [JsonProperty("status")]
        public bool Status { get; set; }
        
        [JsonProperty("token")]
        public string Token { get; set; }
        
        [JsonProperty("game")]
        public Game Game { get; set; }
    }
}