﻿namespace FCG.Games.Infrastructure.ElasticSearch;

public class ElasticSearchSettings
{
    public required string Endpoint { get; init; }

    public string? AccessKey { get; init; }

    public string? Secret { get; init; }

    public required string IndexName { get; init; }
    
    public string? Region { get; init; }
    
    /// <summary>
    /// Quando true, usa ElasticSearch local sem autenticação AWS.
    /// Útil para desenvolvimento e testes locais.
    /// </summary>
    public bool UseLocalMode { get; init; } = false;
}
