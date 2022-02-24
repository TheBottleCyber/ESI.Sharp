using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ESI.Sharp.Extensions;
using ESI.Sharp.Models;
using ESI.Sharp.Models.Authorization;
using ESI.Sharp.Models.Enumerations;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace ESI.Sharp
{
    public class Authorization
    {
        private readonly RestClient _authorizationClient;
        private readonly EsiConfig _config;
        private readonly string _ssoUrl;

        public Authorization(EsiConfig config)
        {
            _config = config;
            _ssoUrl = "login.eveonline.com";

            _authorizationClient = new RestClient($"https://{_ssoUrl}/");
            _authorizationClient.Authenticator = new HttpBasicAuthenticator(_config.ClientId, _config.SecretKey);
        }

        public string CreateAuthorizationUrl(string state, List<string> scope = null)
        {
            if (string.IsNullOrEmpty(state)) throw new ArgumentException("Value cannot be null or empty.", nameof(state));

            var url = $"https://{_ssoUrl}/v2/oauth/authorize/?response_type=code&redirect_uri={Uri.EscapeDataString(_config.CallbackUrl)}&client_id={_config.ClientId}&state={state}";

            if (scope != null)
                url = $"{url}&scope={string.Join("+", scope.Distinct().ToList())}";

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
        /// <param name="token"></param>
        /// <returns></returns>
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

            return new ValidatedToken
            {
                RefreshToken = token.RefreshToken,
                Token = token.AccessToken,
                CharacterName = nameClaim,
                CharacterOwnerHash = ownerClaim,
                CharacterID = int.Parse(subjectClaim.Split(':').Last()),
                ExpiresOn = jwtValidatedToken.ValidTo,
                Scopes = scopesClaim
            };
        }
    }
}