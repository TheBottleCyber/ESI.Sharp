using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Enumerations
{
    /// <summary>
    /// Specifies which server to call the endpoints from
    /// </summary>
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum EsiSource
    {
        /// <summary>
        /// EVE Online test server
        /// </summary>
        [EnumMember(Value = "singularity")] Singularity,

        /// <summary>
        /// EVE Online default server
        /// </summary>
        [EnumMember(Value = "tranquility")] Tranquility,

        /// <summary>
        /// EVE Online special server for China players
        /// </summary>
        [EnumMember(Value = "serenity")] Serenity
    }
}