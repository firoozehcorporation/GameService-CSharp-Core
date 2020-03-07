using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi
{
    /// <summary>
    /// Represents SaveDetails Data Model In Game Service Basic API
    /// </summary>
    [Serializable]
    public class SaveDetails
    {
        /// <summary>
        /// Gets the Game id.
        /// </summary>
        /// <value>the Game id</value>
        [JsonProperty("game")]
        public string Game { get; set; }

        
        /// <summary>
        /// Gets the User id.
        /// </summary>
        /// <value>the User id</value>
        [JsonProperty("user")]
         public string User { get; set; }

        
        /// <summary>
        /// Gets the Save Name.
        /// </summary>
        /// <value>the Save Name</value>
        [JsonProperty("name")]
        public string Name { get; set; }

       
        /// <summary>
        /// Gets the Last Modify Save Time.
        /// </summary>
        /// <value>the Last Modify Save Time</value>
        [JsonProperty("lastmodify")]
        public DateTimeOffset LastModify { get; set; }
        
    }
}