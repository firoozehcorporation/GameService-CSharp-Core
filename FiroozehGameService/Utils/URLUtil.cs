using FiroozehGameService.Models.BasicApi.Buckets;

namespace FiroozehGameService.Utils
{
    internal static class UrlUtil
    {
        internal static string ParseBucketUrl(string bucketId,BucketOption[] options)
        {
            var first = true;
            var url = Models.Consts.Api.Bucket + bucketId;
            if (options == null) return url;

            url += "?";
            
            foreach (var option in options)
            {
                if (first)
                {
                    // To Remove first &
                    url += option.GetParsedData().Substring(1);
                    first = false;
                }
                else url += option.GetParsedData();
            }
            
            return url;
        }
    }
}