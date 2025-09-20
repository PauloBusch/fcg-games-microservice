using FCG.Games.Api._Common;
using FCG.Games.Application.UseCases;
using FCG.Games.Infrastructure.ElasticSearch;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var appSettings = builder.Configuration
    .Get<AppSettings>();

ArgumentNullException.ThrowIfNull(appSettings);

services
    .AddControllers();

services
    .AddOpenApi()
    .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateGameUseCase>());

var elasticSearchSettings = appSettings.ElasticSearchSettings;

services
    .AddElasticSearchModule(elasticSearchSettings);

services
    .AddHealthChecks()
    .AddElasticsearch(
        options =>
        {
            options.UseServer(elasticSearchSettings.Endpoint);
            options.UseApiKey(elasticSearchSettings.ApiKey);
        },
        name: "elasticsearch",
        failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
        tags: [ "ready", "elasticsearch" ]
    );

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseAuthorization();

app.UseHttpMetrics();

app.MapHealthChecks(
    "/health",
    new HealthCheckOptions {
        ResponseWriter = UIResponseWriter
            .WriteHealthCheckUIResponse
    }
);

app.MapMetrics("/metrics");

app.MapControllers();

app.Run();
