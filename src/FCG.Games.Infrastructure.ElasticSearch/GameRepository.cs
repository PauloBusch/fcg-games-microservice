using FCG.Games.Domain;
using OpenSearch.Client;

namespace FCG.Games.Infrastructure.ElasticSearch;

public class GameRepository(IOpenSearchClient elasticClient) : IGameRepository
{
    public async Task<Game?> GetByKeyAsync(Guid key, CancellationToken ct)
    {
        var response = await elasticClient.GetAsync<Game>(key, ct: ct);

        return response.Found ? response.Source : null;
    }

    public async Task<bool> ExistByTitleAsync(string title, Guid? ignoreKey = null, CancellationToken ct = default)
    {
        var response = await elasticClient.SearchAsync<Game>(s => s
            .Query(q => q
                .Bool(b => b
                    .Must(m => m
                        .Term(t => t.Field(f => f.Title).Value(title))
                    )
                    .MustNot(mn => mn
                        .Term(t => t.Key, ignoreKey)
                    )
                )
            ), ct);

        return response.Documents.Any();
    }

    public async Task IndexAsync(Game game, CancellationToken ct)
    {
        await elasticClient.IndexDocumentAsync(game, ct);
    }

    public async Task<IEnumerable<EvaluationDto>> GetEvaluationsByGameKeyAsync(Guid gameKey, CancellationToken ct)
    {
        var game = await GetByKeyAsync(gameKey, ct);
        
        if (game is null)
            return Enumerable.Empty<EvaluationDto>();

        return game.Evaluations.Select(e => new EvaluationDto(
            e.Key,
            (int)e.Stars,
            e.Comment ?? string.Empty,
            DateTime.UtcNow,
            e.User.Name
        ));
    }

    public async Task<DownloadDto?> GetDownloadByGameKeyAsync(Guid gameKey, CancellationToken ct)
    {
        var game = await GetByKeyAsync(gameKey, ct);
        
        if (game is null || !game.Downloads.Any())
            return null;

        var download = game.Downloads.First();

        // TODO: Get from storage service
        return new DownloadDto(
            download.Key, 
            "https://example.com/download",
            0,
            "1.0",
            download.Date
        );
    }

    public async Task UpdateAsync(Game game, CancellationToken ct)
    {
        await elasticClient.UpdateAsync<Game>(game.Key, u => u
            .Doc(game), ct);
    }
}
