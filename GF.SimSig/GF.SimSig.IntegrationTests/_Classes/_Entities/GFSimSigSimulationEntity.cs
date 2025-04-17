namespace GF.SimSig.IntegrationTests._Classes._Entities
{
    public class GFSimSigSimulationEntity : IAsyncLifetime
    {
        private readonly Serilog.ILogger _logger;
        private readonly MongoClient _mongoClient;
        private readonly IConfiguration _configuration;
        private readonly IInjectableTestOutputSink _injectableTestOutputSink;

        private IClientSessionHandle? _session; //stores the MongoDb session
        private string? _testDatabaseName; //stores the name of the master database

        /// <summary>
        /// Default constructor for dependency injection
        /// </summary>
        /// <param name="logger">The <see cref="Serilog.ILogger"/> logger used to log the test</param>
        /// <param name="mongoClient">The mongo client targetting the required cluster</param>
        /// <param name="configuration">The test configuration</param>
        /// <param name="injectableTestOutputSink">The test output sink</param>
        /// <param name="testOutputHelper">The test output helper</param>
        public GFSimSigSimulationEntity(ILogger logger, MongoClient mongoClient, IConfiguration configuration, IInjectableTestOutputSink injectableTestOutputSink, ITestOutputHelper testOutputHelper)
        {
            _logger = logger;
            _mongoClient = mongoClient;
            _configuration = configuration;
            _injectableTestOutputSink = injectableTestOutputSink;
            _injectableTestOutputSink.Inject(testOutputHelper);
        }

        /// <summary>
        /// Initialises the test
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            _testDatabaseName = Guid.NewGuid().ToString();
            _logger.Information<string>("The test database name is: {MasterDatabaseName}.", _testDatabaseName);
            //start a new session
            _session = await _mongoClient.StartSessionAsync(new ClientSessionOptions());

            //start a transaction as per the config
            if (_configuration.GetValue<bool>("TestSettings:WrapInTransaction"))
            {
                _logger.Information("All Mongo CRUD actions are wrapped in a transaction and will be rolled back.");
                _session.StartTransaction(new TransactionOptions());
            }
            else
            {
                _logger.Information<string>("All Mongo CRUD actions are not wrapped in a transaction and any databases / collections created by this test will need to be deleted at {MongoConnection}. If databases are not rolled back you will get the exception 'The InfoFlux Sys Admin already exists for customer {CustomerName} ({CustomerKey})'..", this._configuration.GetConnectionString("MongoDb"));
            }

            _logger.Information("Test successfully initialised.");
        }

        /// <summary>
        /// Cleans up the test
        /// </summary>
        /// <returns></returns>
        public async Task DisposeAsync()
        {
            await Helpers.CleanUpTestAsync(this._session!, this._logger);
            _logger.Information("Test successfully disposed.");
        }
    }
}