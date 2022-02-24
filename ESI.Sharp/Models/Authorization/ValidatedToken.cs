using System;

namespace ESI.Sharp.Models.Authorization
{
    public class ValidatedToken
    {
        public int CharacterID { get; set; }
        public string CharacterName { get; set; }
        public DateTime ExpiresOn { get; set; }
        public string Scopes { get; set; }
        public string CharacterOwnerHash { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}