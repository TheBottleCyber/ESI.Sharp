using System;
using ESI.Sharp.Endpoints;
using ESI.Sharp.Models;
using ESI.Sharp.Models.Authorization;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;

namespace ESI.Sharp
{
    /// <summary>
    /// REST ESI API Client
    /// </summary>
    public class EsiClient
    {
        private ValidatedToken _requestToken;
        private readonly RestClient _restClient;

        /// <summary>
        /// SSO Authorization
        /// </summary>
        public Authorization Authorization { get; set; }
        
        public AllianceEndpoint Alliance { get; set; }
        public AssetsEndpoint Assets { get; set; }
        public StatusEndpoint Status { get; set; }
        public CharacterEndpoint Character { get; set; }
        public ContractsEndpoint Contracts { get; set; }
        public BookmarksEndpoint Bookmarks { get; set; }
        public CalendarEndpoint Calendar { get; set; }
        public ClonesEndpoint Clones { get; set; }
        public ContactsEndpoint Contacts { get; set; }
        public CorporationEndpoint Corporation { get; set; }
        public DogmaEndpoint Dogma { get; set; }
        public FactionWarfareEndpoint FactionWarfare { get; set; }
        public FittingsEndpoint Fittings { get; set; }
        public FleetsEndpoint Fleets { get; set; }
        public IncursionsEndpoint Incursions { get; set; }
        public IndustryEndpoint Industry { get; set; }
        public InsuranceEndpoint Insurance { get; set; }
        public KillmailsEndpoint Killmails { get; set; }
        public LocationEndpoint Location { get; set; }
        public LoyaltyEndpoint Loyalty { get; set; }
        public MailEndpoint Mail { get; set; }
        public MarketEndpoint Market { get; set; }
        public OpportunitiesEndpoint Opportunities { get; set; }
        public PlanetaryEndpoint Planetary { get; set; }
        public RoutesEndpoint Routes { get; set; }
        public SearchEndpoint Search { get; set; }
        public SkillsEndpoint Skills { get; set; }
        public SovereigntyEndpoint Sovereignty { get; set; }
        public UIEndpoint UserInterface { get; set; }
        public UniverseEndpoint Universe { get; set; }
        public WalletEndpoint Wallet { get; set; }
        public WarsEndpoint Wars { get; set; }

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

            _restClient = new RestClient(restClientOptions).AddDefaultHeader(KnownHeaders.Accept, "application/json")
                                                           .AddDefaultHeader("Cache-Control", "no-cache")
                                                           .UseNewtonsoftJson()
                                                           .AddDefaultQueryParameter("datasource", esiConfig.EsiSource.ToString().ToLower());
            
            Authorization = new Authorization(_restClient, esiConfig);

            InitializeEsiEndpoints();
        }

        public void SetRequestToken(ValidatedToken token)
        {
            if (token is not null && string.IsNullOrEmpty(token.AccessToken))
                throw new ArgumentException("AccessToken cannot be null or empty.");

            _requestToken = token;

            if (_requestToken is not null) _restClient.UseAuthenticator(new JwtAuthenticator(_requestToken.AccessToken));
            else _restClient.Authenticator = null;

            InitializeEsiEndpoints();
        }

        private void InitializeEsiEndpoints()
        {
            Alliance = new AllianceEndpoint(_restClient, _requestToken);
            Status = new StatusEndpoint(_restClient, _requestToken);
            Character = new CharacterEndpoint(_restClient, _requestToken);
            Contracts = new ContractsEndpoint(_restClient, _requestToken);
            Assets = new AssetsEndpoint(_restClient, _requestToken);
            Bookmarks = new BookmarksEndpoint(_restClient, _requestToken);
            Calendar = new CalendarEndpoint(_restClient, _requestToken);
            Clones = new ClonesEndpoint(_restClient, _requestToken);
            Contacts = new ContactsEndpoint(_restClient, _requestToken);
            Corporation = new CorporationEndpoint(_restClient, _requestToken);
            Dogma = new DogmaEndpoint(_restClient, _requestToken);
            FactionWarfare = new FactionWarfareEndpoint(_restClient, _requestToken);
            Fittings = new FittingsEndpoint(_restClient, _requestToken);
            Fleets = new FleetsEndpoint(_restClient, _requestToken);
            Incursions = new IncursionsEndpoint(_restClient, _requestToken);
            Industry = new IndustryEndpoint(_restClient, _requestToken);
            Insurance = new InsuranceEndpoint(_restClient, _requestToken);
            Killmails = new KillmailsEndpoint(_restClient, _requestToken);
            Location = new LocationEndpoint(_restClient, _requestToken);
            Loyalty = new LoyaltyEndpoint(_restClient, _requestToken);
            Mail = new MailEndpoint(_restClient, _requestToken);
            Market = new MarketEndpoint(_restClient, _requestToken);
            Opportunities = new OpportunitiesEndpoint(_restClient, _requestToken);
            Planetary = new PlanetaryEndpoint(_restClient, _requestToken);
            Routes = new RoutesEndpoint(_restClient, _requestToken);
            Search = new SearchEndpoint(_restClient, _requestToken);
            Skills = new SkillsEndpoint(_restClient, _requestToken);
            Sovereignty = new SovereigntyEndpoint(_restClient, _requestToken);
            UserInterface = new UIEndpoint(_restClient, _requestToken);
            Universe = new UniverseEndpoint(_restClient, _requestToken);
            Wallet = new WalletEndpoint(_restClient, _requestToken);
            Wars = new WarsEndpoint(_restClient, _requestToken);
        }
    }
}