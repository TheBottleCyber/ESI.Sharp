using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
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
        private readonly RestClient _client;

        internal AllianceEndpoint(RestClient client)
        {
            _client = client;
        }

        /// <summary>
        /// List all active player alliances <br/><br/>
        /// /alliances/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// </summary>
        /// <param name="eTag">ETag from a previous request. A 304 will be returned if this matches the current ETag. Can be empty</param>
        /// <returns><see cref="List{T}">List</see> of <see cref="int">IDs</see> for all player alliances</returns>
        public async Task<EsiResponse<List<int>>> All(string eTag = "")
        {
            var restRequest = new RestRequest("/alliances/").AddHeader("If-None-Match", eTag);
            var esiResponse = await _client.ExecuteAsync(restRequest);

            return new EsiResponse<List<int>>(esiResponse);
        }

        /// <summary>
        /// Public information about an alliance <br/><br/>
        /// /alliances/{alliance_id}/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// </summary>
        /// <param name="allianceId">An EVE alliance ID</param>
        /// <param name="eTag">ETag from a previous request. A 304 will be returned if this matches the current ETag. Can be empty</param>
        /// <example>
        /// <c>var allianceInformation = await esiClient.Alliance.Information(99004425);</c>
        /// </example>
        /// <returns><see cref="Alliance"/> object contains public information about alliance</returns>
        public async Task<EsiResponse<Alliance>> Information(int allianceId, string eTag = "")
        {
            var restRequest = new RestRequest("/alliances/{alliance_id}/").AddUrlSegment("alliance_id", allianceId)
                                                                          .AddHeader("If-None-Match", eTag);
            var esiResponse = await _client.ExecuteAsync(restRequest);

            return new EsiResponse<Alliance>(esiResponse);
        }

        /// <summary>
        /// List all current member corporations of an alliance <br/><br/>
        /// /alliances/{alliance_id}/corporations/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// </summary>
        /// <param name="allianceId">An EVE alliance ID</param>
        /// <param name="eTag">ETag from a previous request. A 304 will be returned if this matches the current ETag. Can be empty</param>
        /// <returns><see cref="List{T}">List</see> of <see cref="int">IDs</see> of alliance corporations</returns>
        public async Task<EsiResponse<List<int>>> Corporations(int allianceId, string eTag = "")
        {
            var restRequest = new RestRequest("/alliances/{alliance_id}/corporations/").AddUrlSegment("alliance_id", allianceId)
                                                                                       .AddHeader("If-None-Match", eTag);
            var esiResponse = await _client.ExecuteAsync(restRequest);

            return new EsiResponse<List<int>>(esiResponse);
        }

        /// <summary>
        /// Get the icon urls for a alliance <br/><br/>
        /// /alliances/{alliance_id}/icons/ <br/><br/>
        /// <c>This route expires daily at 11:05</c>
        /// </summary>
        /// <param name="allianceId">An EVE alliance ID</param>
        /// <param name="eTag">ETag from a previous request. A 304 will be returned if this matches the current ETag. Can be empty</param>
        /// <returns><see cref="Images">Images</see> URLs for the given alliance id and server</returns>
        public async Task<EsiResponse<Images>> Icons(int allianceId, string eTag = "")
        {
            var restRequest = new RestRequest("/alliances/{alliance_id}/icons/").AddUrlSegment("alliance_id", allianceId)
                                                                                .AddHeader("If-None-Match", eTag);
            var esiResponse = await _client.ExecuteAsync(restRequest);

            return new EsiResponse<Images>(esiResponse);
        }
    }
}