using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi.TResponse
{
    [Serializable]
    internal class BucketT<TBucket>
    {
        [JsonProperty("data")] public TBucket BucketData;
        [JsonProperty("status")] public bool Status;
    }
}