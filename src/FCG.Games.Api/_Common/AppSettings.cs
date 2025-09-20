using FCG.Games.Infrastructure.ElasticSearch;

namespace FCG.Games.Api._Common;

public class AppSettings
{
    public required ElasticSearchSettings ElasticSearchSettings { get; set; }
}
