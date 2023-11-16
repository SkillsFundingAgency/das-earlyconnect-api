using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.EarlyConnect.Data.Repository;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.RegistrationExtensions
{
    public static class RepositoryRegistrations
    {
        public static IServiceCollection AddDataRepositories(this IServiceCollection services)
        {
            services.AddTransient<IStudentDataRepository, StudentDataRepository>();
            services.AddTransient<IMetricsDataRepository, MetricsDataRepository>();
            services.AddTransient<IMetricsFlagRepository, MetricsFlagRepository>();
            services.AddTransient<ILEPSDataRepository, LEPSDataRepository>();
            services.AddTransient<ILogDataRepository, LogDataRepository>();

            return services;
        }
    }
}
