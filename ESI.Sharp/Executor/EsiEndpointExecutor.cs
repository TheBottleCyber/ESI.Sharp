using System.Threading.Tasks;
using ESI.Sharp.Executor;
using ESI.Sharp.Models;
using RestSharp;

namespace ESI.Sharp.Helpers
{
    public class EsiEndpointExecutor : IEndpointExecutor
    {
        private readonly RestClient _restClient;
        private readonly EsiClient _esiClient;

        public EsiEndpointExecutor(RestClient restClient, EsiClient esiClient)
        {
            _restClient = restClient;
            _esiClient = esiClient;
        }

        public async Task<EsiResponse<T>> ExecuteEndpointAsync<T>(RestRequest restRequest)
        {
            if (!string.IsNullOrEmpty(_esiClient.ETag))
                restRequest = restRequest.AddOrUpdateHeader("If-None-Match", _esiClient.ETag);

            var restResponse = await _restClient.ExecuteAsync(restRequest);

            return new EsiResponse<T>(restResponse);
        }
    }
}