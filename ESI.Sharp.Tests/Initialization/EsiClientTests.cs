using System;
using ESI.Sharp.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

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
        }
        
        [Test]
        public void InitializeConstructorWithOptions()
        {
            var esiClient = new EsiClient(_esiConfig, new RestClientOptions());
            
            Assert.IsNotNull(esiClient);
        }
        
        [Test]
        public void InitializeConstructorNullConfig()
        {
            Assert.Throws<ArgumentNullException>(() => new EsiClient(null));
        }
        
        [Test]
        public void InitializeConstructorNullOptions()
        {
            Assert.Throws<ArgumentNullException>(() => new EsiClient(_esiConfig, null));
        }
        
        [Test]
        public void InitializeConstructorCheckFields()
        {
            var esiClient = new EsiClient(_esiConfig);
            
            Assert.IsNotNull(esiClient.Alliance);
            Assert.IsNotNull(esiClient.Status);
        }
    }
}