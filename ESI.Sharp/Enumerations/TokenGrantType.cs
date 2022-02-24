using System.Runtime.Serialization;

namespace ESI.Sharp.Models.Enumerations
{
    public enum TokenGrantType
    {
        [EnumMember(Value = "authorization_code")]
        AuthorizationCode,

        [EnumMember(Value = "refresh_token")]
        RefreshToken
    }
}