using FiroozehGameService.Models.Enums.GSLive.TB;
using Newtonsoft.Json;

namespace FiroozehGameService.Models.GSLive.TB
{
    /// <summary>
    ///  Represents PropertyPayload Data Model In GameService TurnBased MultiPlayer System (GSLive)
    /// </summary>
    public class PropertyPayload
    {
        
        /// <summary>
        ///     Gets the Property Type.
        /// </summary>
        /// <value>the PropertyPayload Type</value>
        /// <see cref="Enums.GSLive.TB.PropertyType" />
        [JsonProperty("1")] public PropertyType Type;
        
        
        /// <summary>
        ///     Gets the Property Action
        /// </summary>
        /// <value>the PropertyPayload Action</value>
        /// <see cref="Enums.GSLive.TB.PropertyAction" />
        [JsonProperty("2")] public PropertyAction Action;
        
        
        /// <summary>
        ///     Gets the Property Owner Member.
        /// </summary>
        /// <value>the Property Owner Member</value>
        [JsonProperty("3")] public Member Owner;
        
        
        /// <summary>
        ///     Gets the Property Key.
        /// </summary>
        /// <value>the Property Key</value>
        [JsonProperty("4")] public string Key;

        
        /// <summary>
        ///     Gets the Property Value.
        /// </summary>
        /// <value>the Property Value</value>
        [JsonProperty("5")] public string Value;
    }
}