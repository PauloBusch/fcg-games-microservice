using FCG.Games.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace FCG.Games.Infrastructure.ElasticSearch;

public static class ElasticSearchModule
{
    public static IServiceCollection AddElasticSearchModule(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var settings = configuration
            .GetSection(nameof(ElasticSearchSettings))
            .Get<ElasticSearchSettings>();
        
        ArgumentNullException.ThrowIfNull(settings);
        
        ArgumentNullException.ThrowIfNull(settings.Endpoint);
        
        ArgumentNullException.ThrowIfNull(settings.ApiKey);

        ArgumentNullException.ThrowIfNull(settings.IndexName);

        var connectionSetting = new ConnectionSettings(new Uri(settings.Endpoint))
            .DefaultIndex(settings.IndexName);

        var elasticClient = new ElasticClient(connectionSetting);

        services.AddSingleton<IElasticClient>(elasticClient);

        services.AddScoped<IGameRepository, GameRepository>();

        return services;
    }
}
