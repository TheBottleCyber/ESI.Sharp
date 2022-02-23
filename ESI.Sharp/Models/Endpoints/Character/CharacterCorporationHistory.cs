using System;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Character
{
    /// <summary>
    /// Corporation history for character
    /// </summary>
    public class CharacterCorporationHistory
    {
        /// <summary>
        /// Corporation id of character 
        /// </summary>
        [JsonProperty("corporation_id")]
        public int CorporationId { get; set; }

        /// <summary>
        /// True if the corporation has been deleted
        /// </summary>
        [JsonProperty("is_deleted")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// An incrementing ID that can be used to canonically establish order of records in cases where dates may be ambiguous
        /// </summary>
        [JsonProperty("record_id")]
        public int RecordId { get; set; }

        /// <summary>
        /// Date of the character's entry into the corporation
        /// </summary>
        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }

        public CharacterCorporationHistory() { }

        public CharacterCorporationHistory(int corporationId, bool isDeleted, int recordId, DateTime startDate)
        {
            CorporationId = corporationId;
            IsDeleted = isDeleted;
            RecordId = recordId;
            StartDate = startDate;
        }
    }
}