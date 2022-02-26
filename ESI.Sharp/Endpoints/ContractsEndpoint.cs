using System.Collections.Generic;
using System.Threading.Tasks;
using ESI.Sharp.Models;
using ESI.Sharp.Models.Authorization;
using ESI.Sharp.Models.Endpoints.Contracts;
using ESI.Sharp.Models.Enumerations.Static;
using RestSharp;

namespace ESI.Sharp.Endpoints
{
    public class ContractsEndpoint : EndpointBase
    {
        public ContractsEndpoint(RestClient restClient, ValidatedToken validatedToken) : base(restClient, validatedToken) { }

        /// <summary>
        /// Get public contracts <br/><br/>
        /// /contracts/public/{region_id}/ <br/><br/>
        /// <c>This route is cached for up to 1800 seconds</c>
        /// </summary>
        /// <param name="region">Region id from SDE Enum</param>
        /// <returns>Paginated list of all public contracts in the given region</returns>
        public async Task<EsiResponse<List<Contract>>> Contracts(Region region, int page = 1)
        {
            return await Contracts((int) region, page);
        }

        /// <summary>
        /// Get public contracts <br/><br/>
        /// /contracts/public/{region_id}/ <br/><br/>
        /// <c>This route is cached for up to 1800 seconds</c>
        /// </summary>
        /// <param name="region">Integer region id</param>
        /// <returns>Paginated list of all public contracts in the given region</returns>
        public async Task<EsiResponse<List<Contract>>> Contracts(int region, int page = 1)
        {
            var endpointRequest = new RestRequest("/contracts/public/{region_id}/").AddUrlSegment("region_id", region)
                                                                                   .AddQueryParameter("page", page);

            return await ExecutePublicEndpointAsync<List<Contract>>(endpointRequest);
        }

        /// <summary>
        /// Get public contract items <br/><br/>
        /// /contracts/public/items/{contract_id}/ <br/><br/>
        /// <c>This route is cached for up to 3600 seconds</c>
        /// </summary>
        /// <param name="contract_id">Id of public contract</param>
        /// <returns>Paginated list items of a public contract</returns>
        public async Task<EsiResponse<List<ContractItem>>> ContractItems(int contract_id, int page = 1)
        {
            var endpointRequest = new RestRequest("/contracts/public/items/{contract_id}/").AddUrlSegment("contract_id", contract_id)
                                                                                           .AddQueryParameter("page", page);

            return await ExecutePublicEndpointAsync<List<ContractItem>>(endpointRequest);
        }

        /// <summary>
        /// Get public contract bids <br/><br/>
        /// /contracts/public/bids/{contract_id}/ <br/><br/>
        /// <c>This route is cached for up to 300 seconds</c>
        /// </summary>
        /// <param name="contract_id">Id of public contract</param>
        /// <returns>Lists bids on a public auction contract</returns>
        public async Task<EsiResponse<List<ContractBid>>> ContractBids(int contract_id, int page = 1)
        {
            var endpointRequest = new RestRequest("/contracts/public/bids/{contract_id}/").AddUrlSegment("contract_id", contract_id)
                                                                                          .AddQueryParameter("page", page);

            return await ExecutePublicEndpointAsync<List<ContractBid>>(endpointRequest);
        }
    }
}