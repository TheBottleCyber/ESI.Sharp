using System;
using ESI.Sharp.Endpoints;
using ESI.Sharp.Helpers;
using ESI.Sharp.Models;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;

namespace ESI.Sharp
{
    public class EsiClient
    {
        private string _eTag = string.Empty;

        /// <summary>
        /// Alliance endpoint /alliances/
        /// </summary>
        public AllianceEndpoint Alliance { get; set; }

        /// <summary>
        /// Status endpoint /status/
        /// </summary>
        public StatusEndpoint Status { get; set; }

        /// <summary>
        /// ETag from a previous request. A 304 will be returned if this matches the current ETag. Can be empty string
        /// </summary>
        public string ETag
        {
            get => _eTag;
            set
            {
                if (value == null)
                    throw new ArgumentException("ETag cannot be null", nameof(ETag));

                _eTag = value;
            }
        }

        /// <summary>
        /// Initialize ESI api client by <see cref="EsiConfig"/>
        /// </summary>
        /// <param name="esiConfig">EsiConfig object</param>
        /// <exception cref="ArgumentNullException">throws when trying set <see cref="EsiConfig"/> parameter null</exception>
        public EsiClient(EsiConfig esiConfig) : this(esiConfig, new RestClientOptions()) { }

        /// <summary>
        /// Initialize ESI api client by <see cref="EsiConfig"/> and <see cref="RestClientOptions"/>
        /// </summary>
        /// <param name="esiConfig">EsiConfig object</param>
        /// <param name="restClientOptions">RestClientOptions object</param>
        /// <exception cref="ArgumentNullException">throws when trying set parameters null</exception>
        public EsiClient(EsiConfig esiConfig, RestClientOptions restClientOptions)
        {
            if (esiConfig == null)
                throw new ArgumentNullException(nameof(esiConfig), "EsiClient constructor parameter cannot be null");

            if (restClientOptions == null)
                throw new ArgumentNullException(nameof(restClientOptions), "EsiClient constructor parameter cannot be null");

            restClientOptions.BaseUrl = new Uri(esiConfig.EsiEndpoint);
            restClientOptions.UserAgent = esiConfig.UserAgent;

            var restClient = new RestClient(restClientOptions).AddDefaultHeader(KnownHeaders.Accept, "application/json")
                                                              .AddDefaultHeader("Cache-Control", "no-cache")
                                                              .UseNewtonsoftJson()
                                                              .AddDefaultQueryParameter("datasource", esiConfig.EsiSource.ToEsiValue());

            var executor = new EsiEndpointExecutor(restClient, this);

            Alliance = new AllianceEndpoint(executor);
            Status = new StatusEndpoint(executor);
        }
    }
}