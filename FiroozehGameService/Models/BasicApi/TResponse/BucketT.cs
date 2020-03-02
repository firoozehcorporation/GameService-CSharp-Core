using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi.TResponse
{
    internal class BucketT<TBucket>
    {
        [JsonProperty("status")]
        public bool Status { set; get; }
               
        [JsonProperty("data")]
        public TBucket BucketData { set; get; }
    }
}