using FCG.Games.Infrastructure.ElasticSearch;

namespace FCG.Games.Api._Common.Settings;

public class AppSettings
{
    public required ElasticSearchSettings ElasticSearchSettings { get; set; }

    public required AuthenticationSettings AuthenticationSettings { get; set; }
}
