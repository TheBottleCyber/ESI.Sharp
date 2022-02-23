using System;
using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Character
{
    /// <summary>
    /// Public data for the given character
    /// </summary>
    public class CharacterInformation
    {
        /// <summary>
        /// The character’s alliance ID
        /// </summary>
        [JsonProperty("alliance_id")]
        public int AllianceId { get; set; }

        /// <summary>
        /// Creation date of the character
        /// </summary>
        [JsonProperty("birthday")]
        public DateTime Birthday { get; set; }

        /// <summary>
        /// The character’s blood line
        /// </summary>
        [JsonProperty("bloodline_id")]
        public int BloodlineId { get; set; }

        /// <summary>
        /// The character’s corporation ID
        /// </summary>
        [JsonProperty("corporation_id")]
        public int CorporationId { get; set; }

        /// <summary>
        /// The character’s description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The character’s gender [ female, male ]
        /// </summary>
        [JsonProperty("gender")]
        public string Gender { get; set; }

        /// <summary>
        /// The character’s name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The character’s race id
        /// </summary>
        [JsonProperty("race_id")]
        public int RaceId { get; set; }

        /// <summary>
        /// The CONCORD security status of character
        /// </summary>
        [JsonProperty("security_status")]
        public double SecurityStatus { get; set; }

        /// <summary>
        /// The individual title of the character
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        public CharacterInformation() { }

        public CharacterInformation(int allianceId, DateTime birthday, int bloodlineId, int corporationId, string description,
            string gender, string name, int raceId, double securityStatus, string title)
        {
            AllianceId = allianceId;
            Birthday = birthday;
            BloodlineId = bloodlineId;
            CorporationId = corporationId;
            Description = description;
            Gender = gender;
            Name = name;
            RaceId = raceId;
            SecurityStatus = securityStatus;
            Title = title;
        }
    }
}