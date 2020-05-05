using System;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.BasicApi
{
    /// <summary>
    ///     Represents SaveDetails Data Model In Game Service Basic API
    /// </summary>
    [Serializable]
    public class SaveDetails
    {
        /// <summary>
        ///     Gets the Game id.
        /// </summary>
        /// <value>the Game id</value>
        [JsonProperty("game")] public string Game;


        /// <summary>
        ///     Gets the Last Modify Save Time.
        /// </summary>
        /// <value>the Last Modify Save Time</value>
        [JsonProperty("lastmodify")] public DateTimeOffset LastModify;


        /// <summary>
        ///     Gets the Save Name.
        /// </summary>
        /// <value>the Save Name</value>
        [JsonProperty("name")] public string Name;


        /// <summary>
        ///     Gets the User id.
        /// </summary>
        /// <value>the User id</value>
        [JsonProperty("user")] public string User;
    }
}