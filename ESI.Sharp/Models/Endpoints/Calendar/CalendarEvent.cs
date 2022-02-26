using System;
using ESI.Sharp.Models.Enumerations.Static;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Calendar
{
    public class CalendarEvent
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("event_id")]
        public int EventId { get; set; }

        [JsonProperty("importance")]
        public int Importance { get; set; }

        [JsonProperty("owner_id")]
        public int OwnerId { get; set; }

        [JsonProperty("owner_name")]
        public string OwnerName { get; set; }

        [JsonProperty("owner_type")]
        public OwnerType OwnerType { get; set; }

        [JsonProperty("response")]
        public string Response { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}