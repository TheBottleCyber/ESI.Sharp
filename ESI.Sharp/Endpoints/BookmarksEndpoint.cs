using System.Collections.Generic;
using System.Threading.Tasks;
using ESI.Sharp.Models;
using ESI.Sharp.Models.Authorization;
using ESI.Sharp.Models.Endpoints.Bookmarks;
using ESI.Sharp.Models.Enumerations.Static;
using RestSharp;

namespace ESI.Sharp.Endpoints
{
    public class BookmarksEndpoint : EndpointBase
    {
        public BookmarksEndpoint(RestClient restClient, ValidatedToken validatedToken) : base(restClient, validatedToken) { }

        /// <summary>
        /// List bookmarks <br/><br/>
        /// /characters/{character_id}/bookmarks/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// <br/><c>Requires the following scope: esi-bookmarks.read_character_bookmarks.v1 </c>
        /// </summary>
        /// <returns>List of your character’s personal bookmarks</returns>
        public async Task<EsiResponse<List<Bookmark>>> CharacterBookmarks(int page = 1)
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/bookmarks/").AddUrlSegment("character_id", _validatedToken.CharacterID)
                                                                                          .AddQueryParameter("page", page);

            return await ExecuteAuthorizedEndpointAsync<List<Bookmark>>(endpointRequest, Scope.BookmarksReadCharacterBookmarks);
        }

        /// <summary>
        /// List bookmark folders <br/><br/>
        /// /characters/{character_id}/bookmarks/folders/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// <br/><c>Requires the following scope: esi-bookmarks.read_character_bookmarks.v1 </c>
        /// </summary>
        /// <returns>List of your character’s personal bookmark folders</returns>
        public async Task<EsiResponse<List<CharacterFolder>>> CharacterFolders(int page = 1)
        {
            var endpointRequest = new RestRequest("/characters/{character_id}/bookmarks/folders/").AddUrlSegment("character_id", _validatedToken.CharacterID)
                                                                                                  .AddQueryParameter("page", page);

            return await ExecuteAuthorizedEndpointAsync<List<CharacterFolder>>(endpointRequest, Scope.BookmarksReadCharacterBookmarks);
        }

        /// <summary>
        /// List corporation bookmarks <br/><br/>
        /// /corporations/{corporation_id}/bookmarks/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// <br/><c>Requires the following scope: esi-bookmarks.read_corporation_bookmarks.v1 </c>
        /// </summary>
        /// <returns>List of your corporation’s bookmarks</returns>
        public async Task<EsiResponse<List<Bookmark>>> CorporationBookmarks(int page = 1)
        {
            var endpointRequest = new RestRequest("/corporations/{corporation_id}/bookmarks/").AddUrlSegment("corporation_id", _validatedToken.CharacterCorporationId)
                                                                                              .AddQueryParameter("page", page);

            return await ExecuteAuthorizedEndpointAsync<List<Bookmark>>(endpointRequest, Scope.BookmarksReadCorporationBookmarks);
        }

        /// <summary>
        /// List corporation bookmark folders <br/><br/>
        /// /corporations/{corporation_id}/bookmarks/folders/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// <br/><c>Requires the following scope: esi-bookmarks.read_corporation_bookmarks.v1 </c>
        /// </summary>
        /// <returns>List of your corporation’s bookmark folders</returns>
        public async Task<EsiResponse<List<CorporationFolder>>> CorporationFolders(int page = 1)
        {
            var endpointRequest = new RestRequest("/corporations/{corporation_id}/bookmarks/folders/").AddUrlSegment("corporation_id", _validatedToken.CharacterCorporationId)
                                                                                                      .AddQueryParameter("page", page);

            return await ExecuteAuthorizedEndpointAsync<List<CorporationFolder>>(endpointRequest, Scope.BookmarksReadCorporationBookmarks);
        }
    }
}