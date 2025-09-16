using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FCG.Games.Infrastructure.ElasticSearch;

public static class ElasticSearchModule
{
    public static IServiceCollection AddElasticSearchModule(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        return services;
    }
}
