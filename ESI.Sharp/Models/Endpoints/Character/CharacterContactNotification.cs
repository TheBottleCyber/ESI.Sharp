using System;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Character
{
    public class CharacterContactNotification
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("notification_id")]
        public long NotificationId { get; set; }

        [JsonProperty("send_date")]
        public DateTime SendDate { get; set; }

        [JsonProperty("sender_character_id")]
        public long SenderCharacterId { get; set; }

        /// <summary>
        /// A number representing the standing level the receiver has been added at by the sender. The standing levels are as follows: -10 -> Terrible | -5 -> Bad | 0 -> Neutral | 5 -> Good | 10 -> Excellent
        /// </summary>
        [JsonProperty("standing_level")]
        public decimal StandingLevel { get; set; }
    }
}