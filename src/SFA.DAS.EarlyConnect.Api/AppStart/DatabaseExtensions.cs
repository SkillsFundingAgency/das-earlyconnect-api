using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.EarlyConnect.Data;
using SFA.DAS.EarlyConnect.Domain.Configuration;

namespace SFA.DAS.EarlyConnect.Api.AppStart
{
    public static class DatabaseExtensions
    {
        public static void AddDatabaseRegistration(this IServiceCollection services, EarlyConnectConfiguration config,
            string? environmentName)
        {
            services.AddHttpContextAccessor();
            if (environmentName.Equals("DEV", StringComparison.CurrentCultureIgnoreCase))
            {
                services.AddDbContext<EarlyConnectDataContext>(options => options.UseInMemoryDatabase("SFA.DAS.EmployerProfiles"), ServiceLifetime.Transient);
            }
            else if (environmentName.Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase))
            {
                services.AddDbContext<EarlyConnectDataContext>(
                    options => options.UseSqlServer(config.DatabaseConnectionString), ServiceLifetime.Transient);
            }
            else
            {
                services.AddDbContext<EarlyConnectDataContext>(ServiceLifetime.Transient);
            }

            services.AddTransient<IEarlyConnectDataContext, EarlyConnectDataContext>(provider =>
                provider.GetService<EarlyConnectDataContext>()!);
            services.AddTransient(provider =>
                new Lazy<EarlyConnectDataContext>(provider.GetService<EarlyConnectDataContext>()!));
            services.AddSingleton(new ChainedTokenCredential(
                new ManagedIdentityCredential(),
                new AzureCliCredential(),
                new VisualStudioCodeCredential(),
                new VisualStudioCredential())
            );
        }
    }
}
