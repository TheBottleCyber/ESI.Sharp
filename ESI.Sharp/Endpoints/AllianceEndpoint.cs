using System.Collections.Generic;
using System.Threading.Tasks;
using ESI.Sharp.Helpers;
using ESI.Sharp.Models;
using ESI.Sharp.Models.Endpoints;
using ESI.Sharp.Models.Shared;
using RestSharp;

namespace ESI.Sharp.Endpoints
{
    public class AllianceEndpoint
    {
        private readonly EndpointExecutor _executor;

        internal AllianceEndpoint(EndpointExecutor executor)
        {
            _executor = executor;
        }

        /// <summary>
        /// List all alliances <br/><br/>
        /// /alliances/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// </summary>
        /// <returns>All active player alliances</returns>
        public async Task<EsiResponse<List<int>>> All()
        {
            var endpointRequest = new RestRequest("/alliances/");

            return await _executor.ExecutePublicEndpointAsync<List<int>>(endpointRequest);
        }

        /// <summary>
        /// Get alliance information <br/><br/>
        /// /alliances/{alliance_id}/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// </summary>
        /// <param name="allianceId">An EVE alliance ID</param>
        /// <returns>Public information about an alliance</returns>
        public async Task<EsiResponse<Alliance>> Information(int allianceId)
        {
            var endpointRequest = new RestRequest("/alliances/{alliance_id}/").AddUrlSegment("alliance_id", allianceId);

            return await _executor.ExecutePublicEndpointAsync<Alliance>(endpointRequest);
        }

        /// <summary>
        /// List alliance's corporations <br/><br/>
        /// /alliances/{alliance_id}/corporations/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// </summary>
        /// <param name="allianceId">An EVE alliance ID</param>
        /// <returns>All current member corporations of an alliance</returns>
        public async Task<EsiResponse<List<int>>> Corporations(int allianceId)
        {
            var endpointRequest = new RestRequest("/alliances/{alliance_id}/corporations/").AddUrlSegment("alliance_id", allianceId);

            return await _executor.ExecutePublicEndpointAsync<List<int>>(endpointRequest);
        }

        /// <summary>
        /// Get alliance icon <br/><br/>
        /// /alliances/{alliance_id}/icons/ <br/><br/>
        /// <c>This route expires daily at 11:05</c>
        /// </summary>
        /// <param name="allianceId">An EVE alliance ID</param>
        /// <returns>Icon urls for a alliance</returns>
        public async Task<EsiResponse<Images>> Icons(int allianceId)
        {
            var endpointRequest = new RestRequest("/alliances/{alliance_id}/icons/").AddUrlSegment("alliance_id", allianceId);

            return await _executor.ExecutePublicEndpointAsync<Images>(endpointRequest);
        }
    }
}