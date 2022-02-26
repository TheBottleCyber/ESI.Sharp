using System.Threading.Tasks;
using ESI.Sharp.Models;
using ESI.Sharp.Models.Authorization;
using ESI.Sharp.Models.Endpoints;
using RestSharp;

namespace ESI.Sharp.Endpoints
{
    public class StatusEndpoint : EndpointBase
    {
        public StatusEndpoint(RestClient restClient, ValidatedToken validatedToken) : base(restClient, validatedToken) { }
        
        /// <summary>
        /// Retrieve the uptime and player counts <br/><br/>
        /// /status/ <br/><br/>
        /// <c>This route is cached for up to 30 seconds</c>
        /// </summary>
        /// <returns>EVE Server status</returns>
        public async Task<EsiResponse<Status>> Retrieve()
        {
            var endpointRequest = new RestRequest("/status/");
            
            return await ExecutePublicEndpointAsync<Status>(endpointRequest);
        }
    }
}