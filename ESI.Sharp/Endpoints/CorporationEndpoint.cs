using ESI.Sharp.Models.Authorization;
using RestSharp;

namespace ESI.Sharp.Endpoints
{
    public class CorporationEndpoint : EndpointBase
    {
        public CorporationEndpoint(RestClient restClient, ValidatedToken validatedToken) : base(restClient, validatedToken) { }
    }
}