using System.Collections.Generic;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.TB
{
    /// <summary>
    ///  Represents PropertyData Data Model In GameService TurnBased MultiPlayer System (GSLive)
    /// </summary>
    public class PropertyData
    {
        /// <summary>
        ///     Gets the Property Owner Member.
        /// </summary>
        /// <value>the Property Owner Member</value>
        [JsonProperty("1")] public Member Owner;
        
        
        /// <summary>
        ///     Gets the Properties.
        ///     The Key , Value Dictionary
        /// </summary>
        /// <value>the Properties</value>
        [JsonProperty("2")] public Dictionary<string,string> Properties;
    }
}