using System.Threading.Tasks;
using ESI.Sharp.Models;
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

        public async Task<EsiResponse<T>> ExecuteEndpointAsync<T>(RestRequest restRequest) =>
            new EsiResponse<T>(await _restClient.ExecuteAsync(restRequest));
    }
}