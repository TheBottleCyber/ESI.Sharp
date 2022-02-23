using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Enumerations
{
    /// <summary>
    /// Specifies which contract type
    /// </summary>
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum ContractType
    {
        [EnumMember(Value = "unknown")] Unknown,
        [EnumMember(Value = "item_exchange")] ItemExchange,
        [EnumMember(Value = "auction")] Auction,
        [EnumMember(Value = "courier")] Courier,
        [EnumMember(Value = "loan")] Loan
    }
}