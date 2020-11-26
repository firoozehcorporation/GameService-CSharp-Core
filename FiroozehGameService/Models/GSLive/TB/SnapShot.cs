using System.Collections.Generic;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.TB
{
    /// <summary>
    ///  Represents SnapShot Data Model In GameService TurnBased MultiPlayer System (GSLive)
    /// </summary>
    public class SnapShot
    {
        /// <summary>
        ///     Gets the SnapShot Owner Member.
        /// </summary>
        /// <value>the SnapShot Owner Member</value>
        [JsonProperty("1")] public Member Owner;
        
        
        /// <summary>
        ///     Gets the SnapShot Properties.
        /// </summary>
        /// <value>the SnapShot Properties</value>
        [JsonProperty("2")] public Dictionary<string,string> Properties;
    }
}