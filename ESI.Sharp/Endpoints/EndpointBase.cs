using System;
using System.Threading.Tasks;
using ESI.Sharp.Helpers;
using ESI.Sharp.Models;
using ESI.Sharp.Models.Authorization;
using ESI.Sharp.Models.Enumerations.Static;
using RestSharp;

namespace ESI.Sharp.Endpoints
{
    public class EndpointBase
    {
        protected readonly RestClient _restClient;
        protected readonly ValidatedToken _validatedToken;
        
        public EndpointBase(RestClient restClient, ValidatedToken validatedToken)
        {
            _restClient = restClient;
            _validatedToken = validatedToken;
        }
        
        public async Task<EsiResponse<T>> ExecutePublicEndpointAsync<T>(RestRequest restRequest) =>
            new EsiResponse<T>(await _restClient.ExecuteAsync(restRequest));

        public async Task<EsiResponse<T>> ExecuteAuthorizatedEndpointAsync<T>(RestRequest restRequest, Scope requiredScope)
        {
            if (_validatedToken == null)
                throw new ArgumentException("This endpoint uses authorization which token is null", nameof(_validatedToken));

            if (string.IsNullOrWhiteSpace(_validatedToken.Scopes))
                _validatedToken.Scopes = string.Empty;

            if (string.IsNullOrEmpty(_validatedToken.Scopes))
                throw new UnauthorizedAccessException("This endpoint uses authorization which scopes is null");

            if (!_validatedToken.Scopes.Contains(requiredScope.GetEnumMemberAttribute()))
                throw new UnauthorizedAccessException($"The following scope {requiredScope.GetEnumMemberAttribute()} does not exists in Token scopes");

            return new EsiResponse<T>(await _restClient.ExecuteAsync(restRequest));
        }
    }
}