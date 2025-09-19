using FCG.Games.Domain;
using Nest;

namespace FCG.Games.Infrastructure.ElasticSearch;

public class GameRepository(IElasticClient elasticClient) : IGameRepository
{
    public async Task<bool> ExistByTitleAsync(string title, Guid? ignoreKey = null, CancellationToken ct = default)
    {
        var response = await elasticClient.SearchAsync<Game>(s => s
            .Query(q => q
                .Bool(b => b
                    .Must(m => m
                        .Match(ma => ma
                            .Field(f => f.Title)
                            .Query(title)
                        )
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
}
