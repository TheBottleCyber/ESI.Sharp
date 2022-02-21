using System.Collections.Generic;
using System.Threading.Tasks;
using ESI.Sharp.Models;
using ESI.Sharp.Models.Endpoints;
using RestSharp;

namespace ESI.Sharp.Endpoints
{
    public class StatusEndpoint
    {
        private readonly RestClient _client;

        internal StatusEndpoint(RestClient client)
        {
            _client = client;
        }
        
        /// <summary>
        /// Retrieve the uptime and player counts <br/><br/>
        /// /status/ <br/><br/>
        /// <c>This route is cached for up to 30 seconds</c>
        /// </summary>
        /// <param name="eTag">ETag from a previous request. A 304 will be returned if this matches the current ETag. Can be empty</param>
        /// <returns><see cref="Status"/> object server status</returns>
        public async Task<EsiResponse<Status>> Retrieve(string eTag = "")
        {
            var restRequest = new RestRequest("/status/").AddHeader("If-None-Match", eTag);
            var esiResponse = await _client.ExecuteAsync(restRequest);

            return new EsiResponse<Status>(esiResponse);
        }
    }
}