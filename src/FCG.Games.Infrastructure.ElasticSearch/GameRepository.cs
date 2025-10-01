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
        // Get the game first to access its evaluations
        var game = await GetByKeyAsync(gameKey, ct);
        
        if (game is null)
            return Enumerable.Empty<EvaluationDto>();

        return game.Evaluations.Select(e => new EvaluationDto(
            e.Key,
            (int)e.Stars,  // Convert uint to int for rating
            e.Comment ?? string.Empty,
            DateTime.UtcNow, // Using current time since Evaluation doesn't have CreatedAt
            e.User.Name
        ));
    }

    public async Task<DownloadDto?> GetDownloadByGameKeyAsync(Guid gameKey, CancellationToken ct)
    {
        // Get the game first to access its downloads
        var game = await GetByKeyAsync(gameKey, ct);
        
        if (game is null || !game.Downloads.Any())
            return null;

        var download = game.Downloads.First();
        
        return new DownloadDto(
            download.Key, 
            "https://example.com/download", // Placeholder URL since Download entity doesn't have this property
            0, // Placeholder file size
            "1.0", // Placeholder version
            download.Date
        );
    }

    public async Task UpdateAsync(Game game, CancellationToken ct)
    {
        await elasticClient.UpdateAsync<Game>(game.Key, u => u
            .Doc(game), ct);
    }

    public async Task<IEnumerable<Game>> GetByTitleAsync(Guid catalogKey, string title, CancellationToken cancellationToken)
    {
        var response = await elasticClient.SearchAsync<Game>(s => s
            .Index(catalogKey.ToString().ToLowerInvariant())
            .Query(q => q
                .Match(m => m
                    .Field(f => f.Title)
                    .Query(title)
                )
            ), cancellationToken);

        return response.Documents;
    }

    public async Task<bool> DeleteAsync(Guid catalogKey, Guid gameKey, CancellationToken ct)
    {
        var response = await elasticClient.DeleteAsync<Game>(gameKey, d => d.Index(catalogKey.ToString().ToLowerInvariant()), ct);
        return response.IsValid && response.Result == Result.Deleted;
    }

}
