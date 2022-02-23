using Newtonsoft.Json;

namespace ESI.Sharp.Models.Endpoints.Character
{
    public class Affiliation
    {
        [JsonProperty("alliance_id")]
        public int AllianceId { get; set; }

        [JsonProperty("character_id")]
        public int CharacterId { get; set; }

        [JsonProperty("corporation_id")]
        public int CorporationId { get; set; }
        
        [JsonProperty("faction_id")]
        public int FactionId { get; set; }

        public Affiliation() { }

        public Affiliation(int allianceId, int characterId, int corporationId, int factionId)
        {
            AllianceId = allianceId;
            CharacterId = characterId;
            CorporationId = corporationId;
            FactionId = factionId;
        }
    }
}