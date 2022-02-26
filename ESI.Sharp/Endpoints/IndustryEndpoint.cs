using ESI.Sharp.Models.Authorization;
using RestSharp;

namespace ESI.Sharp.Endpoints
{
    public class IndustryEndpoint : EndpointBase
    {
        public IndustryEndpoint(RestClient restClient, ValidatedToken validatedToken) : base(restClient, validatedToken) { }
    }
}