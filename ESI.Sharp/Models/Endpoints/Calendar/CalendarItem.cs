using System;
using ESI.Sharp.Models.Enumerations.Static;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Calendar
{
    public class CalendarItem
    {
        [JsonProperty("event_date")]
        public DateTime EventDate { get; set; }

        [JsonProperty("event_id")]
        public int EventId { get; set; }

        [JsonProperty("event_response")]
        public EventResponse EventResponse { get; set; }

        [JsonProperty("importance")]
        public int Importance { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}