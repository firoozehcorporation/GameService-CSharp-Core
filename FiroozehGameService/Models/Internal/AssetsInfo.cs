using Newtonsoft.Json;

namespace FiroozehGameService.Models.Internal
{
    /// <summary>
    /// Represents Game Service AssetsInfoData Class
    /// </summary>
    public class AssetInfoData
    {
        /// <summary>
        /// Gets the Asset Size in Bytes.
        /// </summary>
        /// <value>the Asset Size in Bytes</value>
        [JsonProperty("size")]
        public long Size { get; set; }
        
        
        /// <summary>
        /// Gets the Asset Name
        /// </summary>
        /// <value>the Asset Name</value>
        public string Name { get; set; }
        
        
        /// <summary>
        /// Gets the Asset Link for Download
        /// </summary>
        /// <value>the Asset Link for Download</value>
        [JsonProperty("downloadLink")]
        public string Link { get; set; }
    }

    /// <summary>
    /// Represents Game Service AssetInfo Class
    /// </summary>
    public class AssetInfo
    {
        
        /// <summary>
        /// Gets the Asset Info Get Status
        /// </summary>
        /// <value>the Asset Info Get Status</value>
        [JsonProperty("status")]
        public bool Status { set; get; }
        
        
        /// <summary>
        /// Gets the Asset Info Data Class
        /// </summary>
        /// <value>the Asset Info Data Class</value>
        [JsonProperty("data")]
        public AssetInfoData AssetInfoData { set; get; }
    }
}