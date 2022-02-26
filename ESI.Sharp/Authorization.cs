using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ESI.Sharp.Endpoints;
using ESI.Sharp.Helpers;
using ESI.Sharp.Models;
using ESI.Sharp.Models.Authorization;
using ESI.Sharp.Models.Endpoints.Character;
using ESI.Sharp.Models.Enumerations;
using ESI.Sharp.Models.Enumerations.Static;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace ESI.Sharp
{
    public class Authorization
    {
        private readonly CharacterEndpoint _characterEndpoint;
        private readonly RestClient _authorizationClient;
        private readonly EsiConfig _config;
        private readonly string _ssoUrl;

        public Authorization(RestClient restEndpointClient, EsiConfig config, string ssoUrl = "login.eveonline.com")
        {
            _config = config;
            _ssoUrl = ssoUrl;

            _authorizationClient = new RestClient($"https://{_ssoUrl}/")
            {
                Authenticator = new HttpBasicAuthenticator(_config.ClientId, _config.SecretKey)
            };

            _characterEndpoint = new CharacterEndpoint(restEndpointClient, null);
        }

        /// <summary>
        /// Create SSO authorization url to login.eveonline.com
        /// </summary>
        /// <param name="state">Unique string of your choice. State is required by EVE’s SSO to encourage extra security measures</param>
        /// <param name="scopes">Params list of Esi scopes that you would like to request permissions for</param>
        /// <returns>Authorization url</returns>
        public string CreateAuthorizationUrl(string state, params Scope[] scopes)
        {
            if (string.IsNullOrEmpty(state)) throw new ArgumentException("Value cannot be null or empty.", nameof(state));

            var url = $"https://{_ssoUrl}/v2/oauth/authorize/?response_type=code&redirect_uri={Uri.EscapeDataString(_config.CallbackUrl)}&client_id={_config.ClientId}&state={state}";

            if (scopes != null)
            {
                var list = new List<string>();
                foreach (var scope in scopes) list.Add(scope.GetEnumMemberAttribute());

                url = $"{url}&scope={string.Join("%20", list)}";
            }

            return url;
        }

        /// <summary>
        /// SSO Get token
        /// </summary>
        /// <param name="grantType">Token type</param>
        /// <param name="code">The authorization_code or the refresh_token</param>
        /// <returns>Token object</returns>
        public async Task<Token> GetToken(TokenGrantType grantType, string code)
        {
            if (string.IsNullOrEmpty(code)) throw new ArgumentException("Value cannot be null or empty.", nameof(code));

            var body = $"grant_type={grantType.GetEnumMemberAttribute()}";
            switch (grantType)
            {
                case TokenGrantType.AuthorizationCode:
                    body += $"&code={code}";
                    break;
                case TokenGrantType.RefreshToken:
                    body += $"&refresh_token={Uri.EscapeDataString(code)}";
                    break;
            }

            var restRequest = new RestRequest("/v2/oauth/token", Method.Post).AddHeader("Content-Type", "application/x-www-form-urlencoded")
                                                                             .AddStringBody(body, DataFormat.None);

            var response = await _authorizationClient.ExecuteAsync(restRequest);

            if (!response.IsSuccessful)
                throw new HttpRequestException($"Http Status Code for token request is {(int) response.StatusCode} ({response.StatusCode})");

            return JsonConvert.DeserializeObject<Token>(response.Content);
        }

        /// <summary>
        /// SSO Revoke will invalidate the provided token
        /// </summary>
        /// <param name="refreshToken">Token to revoke</param>
        public async Task RevokeToken(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken)) throw new ArgumentException("Value cannot be null or empty.", nameof(refreshToken));

            var body = $"token_type_hint={TokenGrantType.RefreshToken.GetEnumMemberAttribute()}&token={Uri.EscapeDataString(refreshToken)}";

            var restRequest = new RestRequest("/v2/oauth/revoke", Method.Post).AddHeader("Content-Type", "application/x-www-form-urlencoded")
                                                                              .AddStringBody(body, DataFormat.None);

            var response = await _authorizationClient.ExecuteAsync(restRequest);

            if (!response.IsSuccessful)
                throw new HttpRequestException($"Http Status Code for token revoke is {(int) response.StatusCode} ({response.StatusCode})");
        }

        /// <summary>
        /// SSO Validate token and get information about token holder
        /// </summary>
        /// <param name="token">Token object from GetToken</param>
        /// <returns>Validated token including character_id and character_name</returns>
        public async Task<ValidatedToken> ValidateToken(Token token)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));

            var oauthJwksRequest = new RestRequest("/oauth/jwks");
            var oauthJwksResponse = await _authorizationClient.ExecuteAsync(oauthJwksRequest);
            var jsonWebKeySet = new JsonWebKeySet(oauthJwksResponse.Content);

            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidIssuer = _ssoUrl,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = jsonWebKeySet.Keys.First()
            };

            new JwtSecurityTokenHandler().ValidateToken(token.AccessToken, tokenValidationParams, out var validatedToken);
            var jwtValidatedToken = validatedToken as JwtSecurityToken;

            var subjectClaim = jwtValidatedToken.Claims.SingleOrDefault(c => c.Type == "sub").Value;
            var nameClaim = jwtValidatedToken.Claims.SingleOrDefault(c => c.Type == "name").Value;
            var ownerClaim = jwtValidatedToken.Claims.SingleOrDefault(c => c.Type == "owner").Value;

            var returnedScopes = jwtValidatedToken.Claims.Where(c => c.Type == "scp");
            var scopesClaim = string.Join(" ", returnedScopes.Select(s => s.Value));

            var characterId = int.Parse(subjectClaim.Split(':').Last());
            var characterAffiliation = await _characterEndpoint.ExecutePublicEndpointAsync<List<CharacterAffiliation>>(new RestRequest("/characters/affiliation/", Method.Post).AddJsonBody(new[] { characterId }));

            var corporationId = 0;
            var allianceId = 0;

            if (characterAffiliation.StatusCode == HttpStatusCode.OK)
            {
                corporationId = characterAffiliation.Data[0].CorporationId;
                allianceId = characterAffiliation.Data[0].AllianceId;
            }

            return new ValidatedToken
            {
                RefreshToken = token.RefreshToken,
                AccessToken = token.AccessToken,
                CharacterName = nameClaim,
                CharacterCorporationId = corporationId,
                CharacterAllianceId = allianceId,
                CharacterOwnerHash = ownerClaim,
                CharacterID = characterId,
                ExpiresOn = jwtValidatedToken.ValidTo,
                Scopes = scopesClaim
            };
        }
    }
}