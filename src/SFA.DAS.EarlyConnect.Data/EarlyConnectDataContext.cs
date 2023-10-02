using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
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
        private readonly EarlyConnectConfiguration? _configuration;

        public EarlyConnectDataContext()
        {
            
        }

        public EarlyConnectDataContext(DbContextOptions options) : base(options)
        {
            
        }

        public EarlyConnectDataContext(IOptions<EarlyConnectConfiguration> config, DbContextOptions options, ChainedTokenCredential azureServiceTokenProvider, EnvironmentConfiguration environmentConfiguration) : base(options)
        {
            _azureServiceTokenProvider = azureServiceTokenProvider;
            _environmentConfiguration = environmentConfiguration;
            _configuration = config.Value;
        }
    }
}
