using FCG.Games.Api._Common.Extensions;
using FCG.Games.Api._Common.HealthChecks;
using FCG.Games.Api._Common.Middlewares;
using FCG.Games.Api._Common.Pipelines;
using FCG.Games.Api._Common.Settings;
using FCG.Games.Application.UseCases;
using FCG.Games.Application.Validators;
using FCG.Games.Infrastructure.ElasticSearch;
using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddEnvironmentVariables();

var appSettings = builder.Configuration
    .Get<AppSettings>();

ArgumentNullException.ThrowIfNull(appSettings);

var services = builder.Services;

services
    .AddControllers();

services
    .AddOpenApi()
    .AddFcgGamesApiSwagger(appSettings.AuthenticationSettings)
    .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateGameUseCase>())
    .AddValidatorsFromAssemblyContaining<CreateGameInputValidator>()
    .AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidatorPipeline<,>));

services
    .ConfigureAuthentication(appSettings.AuthenticationSettings)
    .ConfigureAuthorization();

var elasticSearchSettings = appSettings.ElasticSearchSettings;

services
    .AddElasticSearchModule(elasticSearchSettings);

services
    .AddHealthChecks()
    .AddCheck<OpenSearchHealthCheck>("opensearch", tags: new[] { "search" });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FCG Games API");

        c.OAuthClientId(appSettings.AuthenticationSettings.Audience);
        c.OAuthAppName("FCG Games API - Swagger");
        c.OAuthUsePkce();
    });

    app.MapOpenApi();
}

app
    .UseMiddleware<ExceptionMiddleware>()
    .UseAuthentication()
    .UseAuthorization()
    .UseHttpMetrics();

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

public partial class Program { }
