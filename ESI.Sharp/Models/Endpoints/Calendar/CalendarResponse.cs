using ESI.Sharp.Models.Enumerations.Static;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Calendar
{
    public class CalendarResponse
    {
        [JsonProperty("character_id")]
        public int CharacterId { get; set; }

        [JsonProperty("event_response")]
        public EventResponse EventResponse { get; set; }
    }
}