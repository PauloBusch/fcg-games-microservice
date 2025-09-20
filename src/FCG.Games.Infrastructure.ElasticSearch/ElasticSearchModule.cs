using FCG.Games.Domain;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace FCG.Games.Infrastructure.ElasticSearch;

public static class ElasticSearchModule
{
    public static IServiceCollection AddElasticSearchModule(
        this IServiceCollection services,
        ElasticSearchSettings settings
    )
    {
        settings.EnsureSettings();

        var connectionSetting = new ConnectionSettings(new Uri(settings.Endpoint))
            .DefaultIndex(settings.IndexName);

        services.AddSingleton<IElasticClient>(_ => new ElasticClient(connectionSetting));

        services.AddScoped<IGameRepository, GameRepository>();

        return services;
    }

    private static ElasticSearchSettings EnsureSettings(this ElasticSearchSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        ArgumentException.ThrowIfNullOrWhiteSpace(settings.Endpoint);

        ArgumentException.ThrowIfNullOrWhiteSpace(settings.ApiKey);

        ArgumentException.ThrowIfNullOrWhiteSpace(settings.IndexName);

        return settings;
    }
}
