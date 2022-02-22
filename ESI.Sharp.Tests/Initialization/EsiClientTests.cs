using System;
using ESI.Sharp.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ESI.Sharp.Tests.Initialization
{
    public class EsiClientTests
    {
        private EsiConfig _esiConfig;

        public EsiClientTests()
        {
            _esiConfig = new EsiConfig("x", "x", "x", "x");
        }

        [Test]
        public void InitializeConstructor()
        {
            var esiClient = new EsiClient(_esiConfig);
            
            Assert.IsNotNull(esiClient);
            Assert.IsNotNull(esiClient.Alliance);
            Assert.IsNotNull(esiClient.Status);
        }
        
        [Test]
        public void InitializeConstructorNullConfig()
        {
            Assert.Throws<ArgumentNullException>(() => new EsiClient(null));
        }

        [Test]
        public void InitializeChangeETag()
        {
            var esiClient = new EsiClient(_esiConfig);
            
            Assert.Throws<ArgumentException>(() => esiClient.ETag = null);

            esiClient.ETag = "some etag";
            
            Assert.AreEqual(esiClient.ETag, "some etag");
        }
    }
}