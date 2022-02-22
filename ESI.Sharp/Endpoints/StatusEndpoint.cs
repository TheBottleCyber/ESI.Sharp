using System.Collections.Generic;
using System.Threading.Tasks;
using ESI.Sharp.Helpers;
using ESI.Sharp.Models;
using ESI.Sharp.Models.Endpoints;
using RestSharp;

namespace ESI.Sharp.Endpoints
{
    public class StatusEndpoint
    {
        private readonly EndpointExecutor _executor;

        internal StatusEndpoint(EndpointExecutor executor)
        {
            _executor = executor;
        }
        
        /// <summary>
        /// Retrieve the uptime and player counts <br/><br/>
        /// /status/ <br/><br/>
        /// <c>This route is cached for up to 30 seconds</c>
        /// </summary>
        /// <returns><see cref="Status"/> object server status</returns>
        public async Task<EsiResponse<Status>> Retrieve()
        {
            var endpointRequest = new RestRequest("/status/");
            
            return await _executor.ExecuteEndpointAsync<Status>(endpointRequest);
        }
    }
}