using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.EarlyConnect.Application.Services.AuthCodeService;
using SFA.DAS.EarlyConnect.Application.Services.DataProtectorService;

namespace SFA.DAS.EarlyConnect.Application.RegistrationExtensions
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthCodeService, AuthCodeService>();
            services.AddTransient<IDataProtectorService, DataProtectorService>();

            return services;
        }
    }
}
