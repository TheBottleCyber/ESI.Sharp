using System;

namespace ESI.Sharp.Models.Authorization
{
    public class ValidatedToken
    {
        public int CharacterID { get; set; }
        public string CharacterName { get; set; }
        public int CharacterCorporationId { get; set; }
        public int CharacterAllianceId { get; set; }
        public DateTime ExpiresOn { get; set; }
        public string Scopes { get; set; }
        public string CharacterOwnerHash { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}