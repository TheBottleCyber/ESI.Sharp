using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints
{
    /// <summary>
    /// Public data about an alliance
    /// <example>Example json body<code>
    /// {
    /// "creator_corporation_id": 98203764,
    /// "creator_id": 1635197119,
    /// "date_founded": "2014-05-06T21:37:13Z",
    /// "executor_corporation_id": 98203764,
    /// "name": "The Bastion",
    /// "ticker": "BASTN"
    /// }
    /// </code></example>
    /// </summary>
    public class Alliance
    {
        /// <summary>
        /// ID of the corporation that created the alliance
        /// </summary>
        [JsonProperty("creator_corporation_id")]
        public int CreatorCorporationId { get; set; }

        /// <summary>
        /// ID of the character that created the alliance
        /// </summary>
        [JsonProperty("creator_id")]
        public int CreatorId { get; set; }

        /// <summary>
        /// Date when alliance was founded
        /// </summary>
        [JsonProperty("date_founded")]
        public DateTime DateFounded { get; set; }

        /// <summary>
        /// The executor corporation ID, if this alliance is not closed
        /// </summary>
        [JsonProperty("executor_corporation_id")]
        public int ExecutorCorporationId { get; set; }

        /// <summary>
        /// Faction ID this alliance is fighting for, if this alliance is enlisted in factional warfare
        /// </summary>
        [JsonProperty("faction_id")]
        public int FactionId { get; set; }

        /// <summary>
        /// The full name of the alliance
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The short name of the alliance
        /// </summary>
        [JsonProperty("ticker")]
        public string Ticker { get; set; }
    }
}