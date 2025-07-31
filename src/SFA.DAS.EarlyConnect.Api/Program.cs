using System.Diagnostics.CodeAnalysis;
using System.Net.Security;
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
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace SFA.DAS.EarlyConnect.Api
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureKestrel(serverOptions =>
                        {
                            serverOptions.AddServerHeader = false;
                            
                            // Disable CBC mode ciphers
                            serverOptions.ConfigureHttpsDefaults(httpsOptions =>
                            {
                                httpsOptions.OnAuthenticate = (context, sslOptions) =>
                                {
                                    // Allow only non-CBC cipher suites
                                    sslOptions.CipherSuitesPolicy = new CipherSuitesPolicy(
                                        new[] {
                                            // TLS 1.3 ciphers
                                            TlsCipherSuite.TLS_AES_128_GCM_SHA256,
                                            TlsCipherSuite.TLS_AES_256_GCM_SHA384,
                                            TlsCipherSuite.TLS_CHACHA20_POLY1305_SHA256,
                                            
                                            // TLS 1.2 non-CBC ciphers
                                            TlsCipherSuite.TLS_ECDHE_ECDSA_WITH_AES_128_GCM_SHA256,
                                            TlsCipherSuite.TLS_ECDHE_ECDSA_WITH_AES_256_GCM_SHA384,
                                            TlsCipherSuite.TLS_ECDHE_RSA_WITH_AES_128_GCM_SHA256,
                                            TlsCipherSuite.TLS_ECDHE_RSA_WITH_AES_256_GCM_SHA384
                                        }
                                    );
                                };
                            });
                        })
                        .UseStartup<Startup>();
                });
    }
}
