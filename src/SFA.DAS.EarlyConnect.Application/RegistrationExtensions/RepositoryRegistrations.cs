using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.EarlyConnect.Data.Repository;
using SFA.DAS.EarlyConnect.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.EarlyConnect.Application.RegistrationExtensions
{
    public static class RepositoryRegistrations
    {
        public static IServiceCollection AddDataRepositories(this IServiceCollection services)
        {
            services.AddTransient<IStudentDataRepository, StudentDataRepository>();
            services.AddTransient<IMetricsDataRepository, MetricsDataRepository>();
            services.AddTransient<ILEPSDataRepository, LEPSDataRepository>();
            services.AddTransient<ILogDataRepository, LogDataRepository>();

            return services;
        }
    }
}
