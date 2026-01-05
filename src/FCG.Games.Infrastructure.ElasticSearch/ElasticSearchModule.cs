﻿using Amazon;
using Amazon.Runtime;
using FCG.Games.Domain;
using Microsoft.Extensions.DependencyInjection;
using OpenSearch.Client;
using OpenSearch.Client.JsonNetSerializer;
using OpenSearch.Net;
using OpenSearch.Net.Auth.AwsSigV4;

namespace FCG.Games.Infrastructure.ElasticSearch;

public static class ElasticSearchModule
{
    public static IServiceCollection AddElasticSearchModule(
        this IServiceCollection services,
        ElasticSearchSettings settings
    )
    {
        settings.EnsureSettings();

        var connectionSetting = GetConnectionString(settings);

        services.AddSingleton<IOpenSearchClient>(_ => new OpenSearchClient(connectionSetting));

        services.AddScoped<IGameRepository, GameRepository>();

        return services;
    }

    private static ConnectionSettings GetConnectionString(ElasticSearchSettings settings)
    {
        ConnectionSettings connectionSetting;
        var pool = new SingleNodeConnectionPool(new Uri(settings.Endpoint));

        if (settings.UseLocalMode)
        {
            // Modo local: sem autenticação AWS (para desenvolvimento/testes)
            connectionSetting = new ConnectionSettings(pool, sourceSerializer: JsonNetSerializer.Default)
                .DefaultMappingFor<Game>(g => g
                    .IdProperty(p => p.Key)
                    .IndexName(settings.IndexName)
                );
        }
        else
        {
            // Modo AWS: com autenticação SigV4
            var credentials = new BasicAWSCredentials(settings.AccessKey, settings.Secret);
            var region = RegionEndpoint.GetBySystemName(settings.Region);
            var awsConnection = new AwsSigV4HttpConnection(credentials, region);
            connectionSetting = new ConnectionSettings(pool, awsConnection, sourceSerializer: JsonNetSerializer.Default)
                .DefaultMappingFor<Game>(g => g
                    .IdProperty(p => p.Key)
                    .IndexName(settings.IndexName)
                );
        }

        return connectionSetting;
    }

    private static ElasticSearchSettings EnsureSettings(this ElasticSearchSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentException.ThrowIfNullOrWhiteSpace(settings.Endpoint);
        ArgumentException.ThrowIfNullOrWhiteSpace(settings.IndexName);

        // Validar credenciais AWS apenas se não estiver em modo local
        if (!settings.UseLocalMode)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(settings.AccessKey);
            ArgumentException.ThrowIfNullOrWhiteSpace(settings.Secret);
            ArgumentException.ThrowIfNullOrWhiteSpace(settings.Region);
        }

        return settings;
    }
}
