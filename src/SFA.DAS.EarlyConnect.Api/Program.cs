using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SFA.DAS.Api.Common.Configuration;
using SFA.DAS.Api.Common.Infrastructure;
using SFA.DAS.EarlyConnect.Api.AppStart;
using SFA.DAS.EarlyConnect.Application.Queries;
using SFA.DAS.EarlyConnect.Data;
using SFA.DAS.EarlyConnect.Domain.Configuration;
using System.Text.Json.Serialization;
using SFA.DAS.Api.Common.AppStart;
using SFA.DAS.EarlyConnect.Api;
using System.Reflection.Metadata;

namespace SFA.DAS.EarlyConnect.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder
                    .UseStartup<Startup>();
                });
    }
}
