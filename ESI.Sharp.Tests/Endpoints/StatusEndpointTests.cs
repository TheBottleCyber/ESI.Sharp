using System.Threading.Tasks;
using ESI.Sharp.Models;
using NUnit.Framework;

namespace ESI.Sharp.Tests.Endpoints
{
    public class StatusEndpointTests
    {
        private EsiClient _esiClient;

        public StatusEndpointTests()
        {
            var config = new EsiConfig("public", "public", "public", "public", esiSource: EsiSource.Tranquility);
            _esiClient = new EsiClient(config);
        }
        
        [Test]
        public async Task EndpointRetrieveMethod()
        {
            var status = await _esiClient.Status.Retrieve();

            Assert.IsFalse(string.IsNullOrEmpty(status.Data.ServerVersion));
        }
    }
}