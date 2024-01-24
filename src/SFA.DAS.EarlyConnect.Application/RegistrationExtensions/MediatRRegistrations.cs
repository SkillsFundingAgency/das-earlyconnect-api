using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.EarlyConnect.Application.Commands.CreateLog;
using SFA.DAS.EarlyConnect.Application.Commands.CreateMetricsData;
using SFA.DAS.EarlyConnect.Application.Commands.CreateOtherStudentTriageData;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentData;
using SFA.DAS.EarlyConnect.Application.Commands.UpdateLog;

namespace SFA.DAS.EarlyConnect.Application.RegistrationExtensions
{
    public static class MediatRRegistrations
    {
        public static IServiceCollection AddMediatRHandlers(this IServiceCollection services)
        {
            services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining<CreateStudentDataCommand>());
            services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining<CreateMetricsDataCommand>());
            services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining<CreateLogCommand>());
            services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining<UpdateLogCommand>());
            services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining<CreateOtherStudentTriageDataCommand>());

            return services;
        }
    }
}
