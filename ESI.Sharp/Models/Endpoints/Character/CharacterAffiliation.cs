using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Character
{
    /// <summary>
    /// Character corporation, alliance and faction IDs
    /// </summary>
    public class CharacterAffiliation
    {
        /// <summary>
        /// The character’s alliance ID, if their corporation is in an alliance
        /// </summary>
        [JsonProperty("alliance_id")]
        public int AllianceId { get; set; }

        /// <summary>
        /// The character’s ID
        /// </summary>
        [JsonProperty("character_id")]
        public int CharacterId { get; set; }

        /// <summary>
        /// The character’s corporation ID
        /// </summary>
        [JsonProperty("corporation_id")]
        public int CorporationId { get; set; }
        
        /// <summary>
        /// The character’s faction ID, if their corporation is in a faction
        /// </summary>
        [JsonProperty("faction_id")]
        public int FactionId { get; set; }

        public CharacterAffiliation() { }

        public CharacterAffiliation(int allianceId, int characterId, int corporationId, int factionId)
        {
            AllianceId = allianceId;
            CharacterId = characterId;
            CorporationId = corporationId;
            FactionId = factionId;
        }
    }
}