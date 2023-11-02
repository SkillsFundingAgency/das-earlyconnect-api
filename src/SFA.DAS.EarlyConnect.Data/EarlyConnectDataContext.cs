using Azure.Core;
using Azure.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using SFA.DAS.EarlyConnect.Domain.Configuration;
using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Data
{
    public class EarlyConnectDataContext : DbContext, IEarlyConnectDataContext
    {
        private const string AzureResource = "https://database.windows.net/";
        private readonly ChainedTokenCredential _azureServiceTokenProvider;
        private readonly EnvironmentConfiguration _environmentConfiguration;
        private readonly EarlyConnectApiConfiguration? _configuration;

        public DbSet<StudentData> StudentData { get; set; }
        public DbSet<ECAPILog> ECAPILogs { get; set; }
        public DbSet<LEPSData> LEPSData { get; set; }
        public DbSet<ApprenticeMetricsData> MetricsData { get; set; }

        public EarlyConnectDataContext()
        {
            
        }

        public EarlyConnectDataContext(DbContextOptions options) : base(options)
        {
            
        }

        public EarlyConnectDataContext(IOptions<EarlyConnectApiConfiguration> config, DbContextOptions options, ChainedTokenCredential azureServiceTokenProvider, EnvironmentConfiguration environmentConfiguration) : base(options)
        {
            _azureServiceTokenProvider = azureServiceTokenProvider;
            _environmentConfiguration = environmentConfiguration;
            _configuration = config.Value;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentData>().ToTable("StudentData");
            modelBuilder.Entity<StudentData>().HasKey(student => student.Id);

            modelBuilder.Entity<ECAPILog>().ToTable("ECAPILog");
            modelBuilder.Entity<ECAPILog>().HasKey(log => log.Id);
            modelBuilder.Entity<ECAPILog>().HasMany(log => log.StudentData)
                .WithOne(student => student.Log)
                .HasForeignKey(lookup => lookup.LogId);
            modelBuilder.Entity<ECAPILog>().HasMany(log => log.ApprenticeMetricsData)
                .WithOne(metric => metric.Log)
                .HasForeignKey(metric => metric.LogId);

            modelBuilder.Entity<ApprenticeMetricsData>().ToTable("ApprenticeMetricsData");
            modelBuilder.Entity<ApprenticeMetricsData>().HasKey(m => m.Id);
            modelBuilder.Entity<ApprenticeMetricsData>().HasMany(m => m.MetricsFlagLookups)
                .WithOne(lookup => lookup.MetricsData)
                .HasForeignKey(lookup => lookup.MetricsId);

            modelBuilder.Entity<MetricsFlagLookup>().ToTable("MetricsFlagLookup");
            modelBuilder.Entity<MetricsFlagLookup>().HasKey(x => x.Id);

            modelBuilder.Entity<ApprenticeMetricsFlag>().ToTable("ApprenticeMetricsFlag");
            modelBuilder.Entity<ApprenticeMetricsFlag>().HasKey(x => x.Id);
            modelBuilder.Entity<ApprenticeMetricsFlag>().HasMany(flag => flag.MetricsFlagLookups)
                .WithOne(lookup => lookup.MetricsFlag)
                .HasForeignKey(lookup => lookup.FlagId);
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
