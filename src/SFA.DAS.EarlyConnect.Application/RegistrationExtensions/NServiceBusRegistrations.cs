using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using SFA.DAS.EarlyConnect.Domain.Configuration;
using SFA.DAS.Notifications.Messages.Commands;
using SFA.DAS.NServiceBus.Configuration;
using SFA.DAS.NServiceBus.Configuration.AzureServiceBus;
using SFA.DAS.NServiceBus.Configuration.NewtonsoftJsonSerializer;
using SFA.DAS.NServiceBus.Hosting;

namespace SFA.DAS.EarlyConnect.Application.RegistrationExtensions
{
    public static class NServiceBusRegistrations
    {
        private const string EndpointName = "SFA.DAS.EarlyConnect.Api";

        public static IServiceCollection AddNServiceBus(this IServiceCollection services)
        {
            return services
               .AddSingleton(p =>
               {
                   var configuration = p.GetService<EarlyConnectApiConfiguration>().NServiceBusConfiguration;
                   var hostingEnvironment = p.GetService<IHostingEnvironment>();

                   var endpointConfiguration = new EndpointConfiguration(EndpointName)
                       .UseErrorQueue($"{EndpointName}-errors")
                       .UseLicense(configuration.NServiceBusLicense)
                       .UseMessageConventions()
                       .UseNewtonsoftJsonSerializer();

                   endpointConfiguration.SendOnly();

                   if (hostingEnvironment.IsDevelopment())
                   {
                       endpointConfiguration.UseLearningTransport(s => s.AddRouting());
                   }
                   else
                   {
                       endpointConfiguration.UseAzureServiceBusTransport(configuration.SharedServiceBusEndpointUrl, s => s.AddRouting());
                   }

                   var endpoint = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

                   return endpoint;
               })
               .AddSingleton<IMessageSession>(s => s.GetService<IEndpointInstance>())
               .AddHostedService<NServiceBusHostedService>();
        }
    }

    public static class RoutingSettingsExtensions
    {
        private const string NotificationsMessageHandler = "SFA.DAS.Notifications.MessageHandlers";

        public static void AddRouting(this RoutingSettings routingSettings)
        {
            routingSettings.RouteToEndpoint(typeof(SendEmailCommand), NotificationsMessageHandler);
        }
    }
}
