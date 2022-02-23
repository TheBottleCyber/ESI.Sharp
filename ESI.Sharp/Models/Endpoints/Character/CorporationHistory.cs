using System;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Character
{
    public class CorporationHistory
    {
        [JsonProperty("corporation_id")]
        public int CorporationId { get; set; }

        [JsonProperty("is_deleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("record_id")]
        public int RecordId { get; set; }

        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }

        public CorporationHistory() { }

        public CorporationHistory(int corporationId, bool isDeleted, int recordId, DateTime startDate)
        {
            CorporationId = corporationId;
            IsDeleted = isDeleted;
            RecordId = recordId;
            StartDate = startDate;
        }
    }
}