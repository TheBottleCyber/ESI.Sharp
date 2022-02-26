using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Enumerations.Static
{
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum OwnerType
    {
        [EnumMember(Value = "eve_server")] Server,
        [EnumMember(Value = "corporation")] Corporation,
        [EnumMember(Value = "faction")] Faction,
        [EnumMember(Value = "character")] Character,
        [EnumMember(Value = "alliance")] Alliance
    }
}