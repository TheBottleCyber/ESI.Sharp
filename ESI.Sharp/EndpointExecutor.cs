using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESI.Sharp.Extensions;
using ESI.Sharp.Models;
using ESI.Sharp.Models.Authorization;
using ESI.Sharp.Models.Enumerations.Static;
using Newtonsoft.Json;
using RestSharp;

namespace ESI.Sharp.Helpers
{
    public class EndpointExecutor
    {
        private readonly RestClient _restClient;

        public EndpointExecutor(RestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<EsiResponse<T>> ExecutePublicEndpointAsync<T>(RestRequest restRequest) =>
            new EsiResponse<T>(await _restClient.ExecuteAsync(restRequest));

        public async Task<EsiResponse<T>> ExecuteAuthorizatedEndpointAsync<T>(RestRequest restRequest, ValidatedToken requestToken, Scope requiredScope)
        {
            if (requestToken == null)
                throw new ArgumentException("This endpoint uses authorization which token is null", nameof(requestToken));

            if (string.IsNullOrWhiteSpace(requestToken.Scopes))
                requestToken.Scopes = string.Empty;

            if (string.IsNullOrEmpty(requestToken.Scopes))
                throw new ArgumentException("This endpoint uses authorization which scopes is null", nameof(requestToken.Scopes));

            if (!requestToken.Scopes.Contains(requiredScope.GetEnumMemberAttribute()))
                throw new UnauthorizedAccessException($"The following scope {requiredScope.GetEnumMemberAttribute()} does not exists in Token scopes");

            return new EsiResponse<T>(await _restClient.ExecuteAsync(restRequest));
        }
    }
}