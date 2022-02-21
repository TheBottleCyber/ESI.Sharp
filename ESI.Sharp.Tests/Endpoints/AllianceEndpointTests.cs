using System;
using System.Net;
using System.Threading.Tasks;
using ESI.Sharp.Models;
using NUnit.Framework;

namespace ESI.Sharp.Tests.Endpoints
{
    public class AllianceEndpointTests
    {
        private EsiClient _esiClient;

        public AllianceEndpointTests()
        {
            var config = new EsiConfig("public", "public", "public", "public", esiSource: EsiSource.Tranquility);
            _esiClient = new EsiClient(config);
        }

        [TestCase(434243723)]
        public async Task EndpointAllMethod(int allianceId)
        {
            var allAlliances = await _esiClient.Alliance.All();
            
            Assert.That(allAlliances.Data, Does.Contain(allianceId));
        }
        
        [TestCase(434243723, "C C P")]
        public async Task EndpointInformationMethod(int allianceId, string ticker)
        {
            var allianceInformation = await _esiClient.Alliance.Information(allianceId);

            Assert.AreEqual(allianceInformation.Data.Ticker, ticker);
        }
        
        [TestCase(434243723, 98356193)]
        public async Task EndpointCorporationsMethod(int allianceId, int corporationId)
        {
            var allianceCorporations = await _esiClient.Alliance.Corporations(allianceId);

            Assert.That(allianceCorporations.Data, Does.Contain(corporationId));
        }
        
        [TestCase(434243723)]
        public async Task EndpointIconsMethod(int allianceId)
        {
            var allianceIcons = await _esiClient.Alliance.Icons(allianceId);

            Assert.IsTrue(!string.IsNullOrEmpty(allianceIcons.Data.x128));
            Assert.IsTrue(!string.IsNullOrEmpty(allianceIcons.Data.x64));
        }
    }
}