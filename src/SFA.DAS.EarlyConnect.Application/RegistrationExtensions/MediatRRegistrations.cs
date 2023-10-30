using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.EarlyConnect.Application.RegistrationExtensions
{
    public static class MediatRRegistrations
    {
        public static IServiceCollection AddMediatRHandlers(this IServiceCollection services)
        {
            services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining<CreateStudentDataCommand>());

            return services;
        }
    }
}
