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

var builder = WebApplication.CreateBuilder(args);

var rootConfiguration = builder.Configuration.LoadConfiguration();
builder.Services.AddOptions();
builder.Services.Configure<EarlyConnectConfiguration>(rootConfiguration.GetSection(nameof(EarlyConnectConfiguration)));
builder.Services.AddSingleton(cfg => cfg.GetService<IOptions<EarlyConnectConfiguration>>().Value);
builder.Services.AddSingleton(new EnvironmentConfiguration(rootConfiguration["EnvironmentName"]));

// Add services to the container.
var earlyConnectConfiguration = rootConfiguration
    .GetSection(nameof(EarlyConnectConfiguration))
    .Get<EarlyConnectConfiguration>();

builder.Services.AddDatabaseRegistration(earlyConnectConfiguration, rootConfiguration["EnvironmentName"]);
builder.Services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining<DummyQuery>());

builder.Services.AddHealthChecks()
    .AddDbContextCheck<EarlyConnectDataContext>();


builder.Services.AddLogging(config =>
{
    config.AddFilter<ApplicationInsightsLoggerProvider>(string.Empty, LogLevel.Information);
    config.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft", LogLevel.Information);
});

if (!(rootConfiguration["EnvironmentName"]!.Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase) ||
      rootConfiguration["EnvironmentName"]!.Equals("DEV", StringComparison.CurrentCultureIgnoreCase)))
{
    var azureAdConfiguration = rootConfiguration
        .GetSection("AzureAd")
        .Get<AzureActiveDirectoryConfiguration>();

    var policies = new Dictionary<string, string>
    {
        {PolicyNames.Default, RoleNames.Default},
    };
    builder.Services.AddAuthentication(azureAdConfiguration, policies);
}

builder.Services
    .AddMvc(o =>
    {
        if (!(rootConfiguration["EnvironmentName"]!.Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase) ||
              rootConfiguration["EnvironmentName"]!.Equals("DEV", StringComparison.CurrentCultureIgnoreCase)))
        {
            o.Conventions.Add(new AuthorizeControllerModelConvention(new List<string>()));
        }
        o.Conventions.Add(new ApiExplorerGroupPerVersionConvention());
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddApplicationInsightsTelemetry();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EarlyConnectApi", Version = "v1" });
    c.OperationFilter<SwaggerVersionHeaderFilter>();
});

builder.Services.AddApiVersioning(opt => {
    opt.ApiVersionReader = new HeaderApiVersionReader("X-Version");
});


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EarlyConnectApi v1");
    c.RoutePrefix = string.Empty;
});


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseAuthentication();


app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();

if (!app.Configuration["EnvironmentName"]!.Equals("DEV", StringComparison.CurrentCultureIgnoreCase))
{
    app.UseHealthChecks();
}


app.UseEndpoints(config =>
{
    config.MapControllerRoute(name: "default", pattern: "api/{controller=Users}/{action=Index}/{id?}");
});

app.Run();
