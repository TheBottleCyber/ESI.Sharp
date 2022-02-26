using ESI.Sharp.Models.Authorization;
using RestSharp;

namespace ESI.Sharp.Endpoints
{
    public class RoutesEndpoint : EndpointBase
    {
        public RoutesEndpoint(RestClient restClient, ValidatedToken validatedToken) : base(restClient, validatedToken) { }
        
        
    }
}