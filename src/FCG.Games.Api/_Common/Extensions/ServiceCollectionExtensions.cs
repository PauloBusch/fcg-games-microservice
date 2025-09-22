using Microsoft.OpenApi.Models;

namespace FCG.Games.Api._Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFcgGamesApiSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "FCG Games API", Version = "v1" });
        });

        return services;
    }
}
