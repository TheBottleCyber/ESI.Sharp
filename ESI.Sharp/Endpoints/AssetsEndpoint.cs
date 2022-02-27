using System.Collections.Generic;
using System.Threading.Tasks;
using ESI.Sharp.Models;
using ESI.Sharp.Models.Authorization;
using ESI.Sharp.Models.Endpoints.Assets;
using ESI.Sharp.Models.Enumerations.Static;
using RestSharp;

namespace ESI.Sharp.Endpoints
{
    public class AssetsEndpoint : EndpointBase
    {
        public AssetsEndpoint(RestClient restClient, ValidatedToken validatedToken) : base(restClient, validatedToken) { }

        /// <summary>
        /// Get character assets <br/><br/>
        /// /characters/{character_id}/assets/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// <br/><c>Requires the following scope: esi-assets.read_assets.v1 </c>
        /// </summary>
        /// <returns>Paginated list of the characters assets</returns>
        public async Task<EsiResponse<List<AssetsItem>>> CharacterAssets(int page = 1)
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/assets/").AddUrlSegment("character_id", _validatedToken.CharacterID)
                                                                                       .AddQueryParameter("page", page);

            return await ExecuteAuthorizedEndpointAsync<List<AssetsItem>>(endpointRequest, Scope.AssetsReadAssets);
        }

        /// <summary>
        /// Get character asset locations <br/><br/>
        /// /characters/{character_id}/assets/locations/ <br/><br/>
        /// <c>This route is not cached</c>
        /// <br/><c>Requires the following scope: esi-assets.read_assets.v1 </c>
        /// </summary>
        /// <returns>Locations for a set of item ids, which you can get from character assets endpoint. Coordinates for items in hangars or stations are set to (0,0,0)</returns>
        public async Task<EsiResponse<List<AssetsItemLocation>>> CharacterAssetsLocations(params long[] itemIds)
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/assets/locations/", Method.Post).AddUrlSegment("character_id", _validatedToken.CharacterID)
                                                                                                              .AddJsonBody(itemIds);

            return await ExecuteAuthorizedEndpointAsync<List<AssetsItemLocation>>(endpointRequest, Scope.AssetsReadAssets);
        }

        /// <summary>
        /// Get character asset names <br/><br/>
        /// /characters/{character_id}/assets/names/ <br/><br/>
        /// <c>This route is not cached</c>
        /// <br/><c>Requires the following scope: esi-assets.read_assets.v1 </c>
        /// </summary>
        /// <returns>Names for a set of item ids, which you can get from character assets endpoint. Typically used for items that can customize names, like containers or ships</returns>
        public async Task<EsiResponse<List<AssetsItemName>>> CharacterAssetsNames(params long[] itemIds)
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/assets/names/", Method.Post).AddUrlSegment("character_id", _validatedToken.CharacterID)
                                                                                                          .AddJsonBody(itemIds);

            return await ExecuteAuthorizedEndpointAsync<List<AssetsItemName>>(endpointRequest, Scope.AssetsReadAssets);
        }

        /// <summary>
        /// Get corporation assets <br/><br/>
        /// /corporations/{corporation_id}/assets/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// <br/><c>Requires one of the following EVE corporation role(s): Director </c>
        /// <br/><c>Requires the following scope: esi-assets.read_corporation_assets.v1 </c>
        /// </summary>
        /// <returns>Paginated list of the corporation assets</returns>
        public async Task<EsiResponse<List<AssetsItem>>> CorporationAssets(int page = 1)
        {
            var endpointRequest = new RestRequest("/corporations/{corporation_id}/assets/").AddUrlSegment("corporation_id", _validatedToken.CharacterCorporationId)
                                                                                           .AddQueryParameter("page", page);

            return await ExecuteAuthorizedEndpointAsync<List<AssetsItem>>(endpointRequest, Scope.AssetsReadCorporationAssets);
        }

        /// <summary>
        /// Get corporation asset locations <br/><br/>
        /// /corporations/{corporation_id}/assets/locations/ <br/><br/>
        /// <c>This route is not cached</c>
        /// <br/><c>Requires one of the following EVE corporation role(s): Director </c>
        /// <br/><c>Requires the following scope: esi-assets.read_corporation_assets.v1 </c>
        /// </summary>
        /// <returns>Locations for a set of item ids, which you can get from corporation assets endpoint. Coordinates for items in hangars or stations are set to (0,0,0)</returns>
        public async Task<EsiResponse<List<AssetsItemLocation>>> CorporationAssetsLocations(params long[] itemIds)
        {
            var endpointRequest = new RestRequest("/corporations/{corporation_id}/assets/locations/", Method.Post).AddUrlSegment("corporation_id", _validatedToken.CharacterCorporationId)
                                                                                                                  .AddJsonBody(itemIds);

            return await ExecuteAuthorizedEndpointAsync<List<AssetsItemLocation>>(endpointRequest, Scope.AssetsReadCorporationAssets);
        }

        /// <summary>
        /// Get corporation asset names <br/><br/>
        /// /corporations/{corporation_id}/assets/names/ <br/><br/>
        /// <c>This route is not cached</c>
        /// <br/><c>Requires one of the following EVE corporation role(s): Director </c>
        /// <br/><c>Requires the following scope: esi-assets.read_corporation_assets.v1 </c>
        /// </summary>
        /// <returns>Names for a set of item ids, which you can get from corporation assets endpoint. Only valid for items that can customize names, like containers or ships</returns>
        public async Task<EsiResponse<List<AssetsItemName>>> CorporationAssetsNames(params long[] itemIds)
        {
            var endpointRequest = new RestRequest("/corporations/{corporation_id}/assets/names/", Method.Post).AddUrlSegment("corporation_id", _validatedToken.CharacterCorporationId)
                                                                                                              .AddJsonBody(itemIds);

            return await ExecuteAuthorizedEndpointAsync<List<AssetsItemName>>(endpointRequest, Scope.AssetsReadCorporationAssets);
        }
    }
}