using Newtonsoft.Json;

namespace FiroozehGameService.Models.Command
{
    public class Data
    {
        [JsonProperty("size")]
        public long Size { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class Download
    {
        [JsonProperty("data")]
        public Data Data { set; get; }
    }
}