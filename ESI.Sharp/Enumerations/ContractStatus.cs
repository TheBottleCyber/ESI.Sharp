using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Enumerations
{
    /// <summary>
    /// Status of the contract
    /// </summary>
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum ContractStatus
    {
        [EnumMember(Value = "outstanding")] Outstanding,
        [EnumMember(Value = "in_progress")] InProgress,
        [EnumMember(Value = "finished_issuer")] FinishedIssuer,
        [EnumMember(Value = "finished_contractor")] FinishedContractor,
        [EnumMember(Value = "finished")] Finished,
        [EnumMember(Value = "cancelled")] Cancelled,
        [EnumMember(Value = "rejected")] Rejected,
        [EnumMember(Value = "failed")] Failed,
        [EnumMember(Value = "deleted")] Deleted,
        [EnumMember(Value = "reversed")] Reversed
    }
}