namespace FCG.Games.Infrastructure.ElasticSearch;

public class ElasticSearchSettings
{
    public required string Endpoint { get; set; }

    public required string ApiKey { get; set; }

    public required string IndexName { get; set; }
}
