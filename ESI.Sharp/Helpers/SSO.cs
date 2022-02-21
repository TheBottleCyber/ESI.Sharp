using System;
using System.Text;
using ESI.Sharp.Models;
using RestSharp;

namespace ESI.Sharp.Helpers
{
    public class SSO
    {
        private readonly RestClient _client;
        private readonly EsiConfig _esiConfig;
        private readonly string _clientKey;
        
        public SSO(RestClient client, EsiConfig esiConfig)
        {
            _client = client;
            _esiConfig = esiConfig;
            
            _clientKey = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{esiConfig.ClientId}:{esiConfig.SecretKey}"));
        }
    }
}