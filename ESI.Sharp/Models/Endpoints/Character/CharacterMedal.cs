using System;
using System.Collections.Generic;
using ESI.Sharp.Models.Shared;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Character
{
    public class CharacterMedal
    {
        /// <summary>
        /// Id of the corporation that issued the medal
        /// </summary>
        [JsonProperty("corporation_id")]
        public int CorporationId { get; set; }

        /// <summary>
        /// Date of issue of the medal
        /// </summary>
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Description of the medal
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("graphics")]
        public List<GraphicLayer> Graphics { get; set; }

        /// <summary>
        /// Issuer id
        /// </summary>
        [JsonProperty("issuer_id")]
        public int IssuerId { get; set; }

        /// <summary>
        /// Medal id
        /// </summary>
        [JsonProperty("medal_id")]
        public int MedalId { get; set; }

        /// <summary>
        /// The reason for awarding the medal
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// Public or private display of the medal
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Medal's title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}