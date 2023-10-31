using Azure.Core;
using Azure.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SFA.DAS.EarlyConnect.Domain.Configuration;

namespace SFA.DAS.EarlyConnect.Data
{
    public interface IEarlyConnectDataContext
    {
    }

    public class EarlyConnectDataContext : DbContext, IEarlyConnectDataContext
    {
        private const string AzureResource = "https://database.windows.net/";
        private readonly ChainedTokenCredential _azureServiceTokenProvider;
        private readonly EnvironmentConfiguration _environmentConfiguration;
        private readonly EarlyConnectApi? _configuration;

        public EarlyConnectDataContext()
        {
        }

        public EarlyConnectDataContext(DbContextOptions options) : base(options)
        {
        }

        public EarlyConnectDataContext(IOptions<EarlyConnectApi> config,
            DbContextOptions options,
            ChainedTokenCredential azureServiceTokenProvider,
            EnvironmentConfiguration environmentConfiguration) : base(options)
        {
            _azureServiceTokenProvider = azureServiceTokenProvider;
            _environmentConfiguration = environmentConfiguration;
            _configuration = config.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();

            if (_configuration == null) return;

            var connection = new SqlConnection
            {
                ConnectionString = _configuration.DatabaseConnectionString
            };

            if (!(_environmentConfiguration.EnvironmentName.Equals("DEV", StringComparison.CurrentCultureIgnoreCase)
                || _environmentConfiguration.EnvironmentName.Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase)))
            {
                connection.AccessToken = _azureServiceTokenProvider
                    .GetTokenAsync(new TokenRequestContext(scopes: new string[] { AzureResource })).Result.Token;
            }

            optionsBuilder.UseSqlServer(connection, options =>
                options.EnableRetryOnFailure(
                    5,
                    TimeSpan.FromSeconds(20),
                    null
                ));
        }
    }
}
