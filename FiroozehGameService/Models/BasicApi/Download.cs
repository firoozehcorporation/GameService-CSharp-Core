using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi
{
    public class Data
    {
        [JsonProperty("size")]
        public long Size { get; set; }
        
        [JsonProperty("downloadLink")]
        public string Link { get; set; }
    }

    public class Download
    {
        [JsonProperty("status")]
        public bool Status { set; get; }
        
        [JsonProperty("data")]
        public Data Data { set; get; }
    }
}