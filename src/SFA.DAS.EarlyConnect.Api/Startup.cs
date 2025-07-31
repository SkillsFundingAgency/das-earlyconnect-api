using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;
using SFA.DAS.Configuration.AzureTableStorage;
using SFA.DAS.EarlyConnect.Api.AppStart;
using SFA.DAS.EarlyConnect.Data;
using SFA.DAS.EarlyConnect.Domain.Configuration;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using SFA.DAS.Api.Common.Configuration;
using SFA.DAS.Api.Common.Infrastructure;
using System.Text.Json.Serialization;
using SFA.DAS.Api.Common.AppStart;
using Microsoft.Extensions.Logging.ApplicationInsights;
using SFA.DAS.EarlyConnect.Application.Commands;
using SFA.DAS.EarlyConnect.Domain.Interfaces;
using SFA.DAS.EarlyConnect.Application.RegistrationExtensions;
using System.Runtime.InteropServices; // For OS platform detection


namespace SFA.DAS.EarlyConnect.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration.LoadConfiguration();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<EarlyConnectApiConfiguration>(_configuration.GetSection("EarlyConnectApi"));
            services.AddSingleton(cfg => cfg.GetService<IOptions<EarlyConnectApiConfiguration>>().Value);
            services.AddSingleton(new EnvironmentConfiguration(_configuration["EnvironmentName"]));

            var earlyConnectConfiguration = _configuration
                .GetSection("EarlyConnectApi")
                .Get<EarlyConnectApiConfiguration>();

            services.AddDatabaseRegistration(earlyConnectConfiguration, _configuration["EnvironmentName"]);
            services.AddMediatRHandlers();
            services.AddDataRepositories();
            services.AddServices();
            services.AddNServiceBus();

            services.AddHealthChecks()
                .AddDbContextCheck<EarlyConnectDataContext>();

            services.AddLogging(config =>
            {
                config.AddFilter<ApplicationInsightsLoggerProvider>(string.Empty, LogLevel.Information);
                config.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft", LogLevel.Information);
            });

            if (!ConfigurationIsLocalOrDev())
            {
                var azureAdConfiguration = _configuration
                    .GetSection("AzureAd")
                    .Get<AzureActiveDirectoryConfiguration>();

                var policies = new Dictionary<string, string>
                {
                    {PolicyNames.Default, RoleNames.Default},
                };
                services.AddAuthentication(azureAdConfiguration, policies);
            }

            services
                .AddMvc(o =>
                {
                    if (!ConfigurationIsLocalOrDev())
                    {
                        o.Conventions.Add(new AuthorizeControllerModelConvention(new List<string>()));
                    }
                    o.Conventions.Add(new ApiExplorerGroupPerVersionConvention());
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddApplicationInsightsTelemetry();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EarlyConnectApi", Version = "v1" });
                c.OperationFilter<SwaggerVersionHeaderFilter>();
            });

            services.AddApiVersioning(opt =>
            {
                opt.ApiVersionReader = new HeaderApiVersionReader("X-Version");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            // Security headers middleware - placed at the start of pipeline
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Remove("Server");
                context.Response.Headers.Remove("X-Powered-By");
                context.Response.Headers.Remove("X-AspNet-Version");
                context.Response.Headers.Remove("X-AspNetMvc-Version");
                context.Response.Headers.Remove("X-Version");
                
                // Additional security headers
                context.Response.Headers["Strict-Transport-Security"] = "max-age=31536000; includeSubDomains";
                context.Response.Headers["X-Content-Type-Options"] = "nosniff";
                context.Response.Headers["X-Frame-Options"] = "DENY";
                context.Response.Headers["Content-Security-Policy"] = "default-src 'self'";
                context.Response.Headers["Referrer-Policy"] = "no-referrer";
                
                await next();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EarlyConnectApi v1");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            if (!_configuration["EnvironmentName"]!.Equals("DEV", StringComparison.CurrentCultureIgnoreCase))
            {
                app.UseHealthChecks();
            }

            app.UseEndpoints(config =>
            {
                config.MapControllerRoute(name: "default", pattern: "api/{controller=Users}/{action=Index}/{id?}");
            });
        }

        private bool ConfigurationIsLocalOrDev()
        {
            return _configuration["EnvironmentName"].Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase) ||
                   _configuration["EnvironmentName"].Equals("DEV", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}