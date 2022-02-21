using System;
using ESI.Sharp.Models;
using NUnit.Framework;

namespace ESI.Sharp.Tests.Initialization
{
    public class EsiConfigTests
    {
        [TestCase("", "", "", "", "")]
        [TestCase(null, null, null, null, null)]
        public void InitializeConstructorNullParameters(string clientId, string secretKey, string callbackUrl, string userAgent, string esiEndpoint)
        {
            Assert.Throws<ArgumentException>(() => new EsiConfig(clientId, secretKey, callbackUrl, userAgent, esiEndpoint));
        }
        
        [TestCase("clientId", "secretKey", "callbackUrl", "userAgent", "esiEndpoint")]
        public void InitializeConstructor(string clientId, string secretKey, string callbackUrl, string userAgent, string esiEndpoint)
        {
            var esiConfig = new EsiConfig(clientId, secretKey, callbackUrl, userAgent, esiEndpoint);
            
            Assert.IsNotNull(esiConfig);
            
            Assert.AreEqual(clientId, esiConfig.ClientId);
            Assert.AreEqual(secretKey, esiConfig.SecretKey);
            Assert.AreEqual(callbackUrl, esiConfig.CallbackUrl);
            Assert.AreEqual(userAgent, esiConfig.UserAgent);
        }
        
        [TestCase("clientId", "secretKey", "callbackUrl", "userAgent", "esiEndpoint")]
        public void InitializeSetters(string clientId, string secretKey, string callbackUrl, string userAgent, string esiEndpoint)
        {
            var esiConfig = new EsiConfig
            {
                ClientId = clientId, 
                SecretKey = secretKey, 
                CallbackUrl = callbackUrl, 
                UserAgent = userAgent,
                EsiEndpoint = esiEndpoint
            };
            
            Assert.IsNotNull(esiConfig);
            
            Assert.AreEqual(clientId, esiConfig.ClientId);
            Assert.AreEqual(secretKey, esiConfig.SecretKey);
            Assert.AreEqual(callbackUrl, esiConfig.CallbackUrl);
            Assert.AreEqual(userAgent, esiConfig.UserAgent);
        }
        
        [TestCase("", "", "", "", "")]
        [TestCase(null, null, null, null, null)]
        public void InitializeSettersNullFields(string clientId, string secretKey, string callbackUrl, string userAgent, string esiEndpoint)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new EsiConfig
                {
                    ClientId = clientId, 
                    SecretKey = secretKey, 
                    CallbackUrl = callbackUrl, 
                    UserAgent = userAgent,
                    EsiEndpoint = esiEndpoint
                };
            });
        }
    }
}