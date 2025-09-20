using FCG.Games.Infrastructure.ElasticSearch;

namespace FCG.Games.Api.Contracts;

public class AppSettings
{
    public required ElasticSearchSettings ElasticSearchSettings { get; set; }
}
