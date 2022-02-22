using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using ESI.Sharp.Executor;
using ESI.Sharp.Helpers;
using ESI.Sharp.Models;
using ESI.Sharp.Models.Endpoints;
using ESI.Sharp.Models.Shared;
using RestSharp;

namespace ESI.Sharp.Endpoints
{
    public class AllianceEndpoint
    {
        private readonly IEndpointExecutor _executor;

        internal AllianceEndpoint(IEndpointExecutor executor)
        {
            _executor = executor;
        }

        /// <summary>
        /// List all active player alliances <br/><br/>
        /// /alliances/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// </summary>
        /// <returns><see cref="List{T}">List</see> of <see cref="int">IDs</see> for all player alliances</returns>
        public async Task<EsiResponse<List<int>>> All()
        {
            var endpointRequest = new RestRequest("/alliances/");

            return await _executor.ExecuteEndpointAsync<List<int>>(endpointRequest);
        }

        /// <summary>
        /// Public information about an alliance <br/><br/>
        /// /alliances/{alliance_id}/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// </summary>
        /// <param name="allianceId">An EVE alliance ID</param>
        /// <example>
        /// <c>var allianceInformation = await esiClient.Alliance.Information(99004425);</c>
        /// </example>
        /// <returns><see cref="Alliance"/> object contains public information about alliance</returns>
        public async Task<EsiResponse<Alliance>> Information(int allianceId)
        {
            var endpointRequest = new RestRequest("/alliances/{alliance_id}/").AddUrlSegment("alliance_id", allianceId);

            return await _executor.ExecuteEndpointAsync<Alliance>(endpointRequest);
        }

        /// <summary>
        /// List all current member corporations of an alliance <br/><br/>
        /// /alliances/{alliance_id}/corporations/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// </summary>
        /// <param name="allianceId">An EVE alliance ID</param>
        /// <returns><see cref="List{T}">List</see> of <see cref="int">IDs</see> of alliance corporations</returns>
        public async Task<EsiResponse<List<int>>> Corporations(int allianceId)
        {
            var endpointRequest = new RestRequest("/alliances/{alliance_id}/corporations/").AddUrlSegment("alliance_id", allianceId);

            return await _executor.ExecuteEndpointAsync<List<int>>(endpointRequest);
        }

        /// <summary>
        /// Get the icon urls for a alliance <br/><br/>
        /// /alliances/{alliance_id}/icons/ <br/><br/>
        /// <c>This route expires daily at 11:05</c>
        /// </summary>
        /// <param name="allianceId">An EVE alliance ID</param>
        /// <returns><see cref="Images">Images</see> URLs for the given alliance id and server</returns>
        public async Task<EsiResponse<Images>> Icons(int allianceId)
        {
            var endpointRequest = new RestRequest("/alliances/{alliance_id}/icons/").AddUrlSegment("alliance_id", allianceId);

            return await _executor.ExecuteEndpointAsync<Images>(endpointRequest);
        }
    }
}