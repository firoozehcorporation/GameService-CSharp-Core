using Newtonsoft.Json;

namespace FiroozehGameService.Models.Internal
{
    internal class Data
    {
        [JsonProperty("size")]
        public long Size { get; set; }
        
        [JsonProperty("downloadLink")]
        public string Link { get; set; }
    }

    internal class Download
    {
        [JsonProperty("status")]
        public bool Status { set; get; }
        
        [JsonProperty("data")]
        public Data Data { set; get; }
    }
}