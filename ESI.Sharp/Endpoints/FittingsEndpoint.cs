using ESI.Sharp.Models.Authorization;
using RestSharp;

namespace ESI.Sharp.Endpoints
{
    public class FittingsEndpoint : EndpointBase
    {
        public FittingsEndpoint(RestClient restClient, ValidatedToken validatedToken) : base(restClient, validatedToken) { }
    }
}