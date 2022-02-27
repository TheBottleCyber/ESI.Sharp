using System.Collections.Generic;
using ESI.Sharp.Models.Enumerations.Static;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Contacts
{
    public class Contact
    {
        [JsonProperty("standing")]
        public decimal Standing { get; set; }

        [JsonProperty("contact_type")]
        public ContactType ContactType { get; set; }

        [JsonProperty("contact_id")]
        public int ContactId { get; set; }

        [JsonProperty("is_watched")]
        public bool IsWatched { get; set; }

        [JsonProperty("is_blocked")]
        public bool IsBlocked { get; set; }

        [JsonProperty("label_ids")]
        public List<long> LabelIds { get; set; } = new List<long>();
    }
}