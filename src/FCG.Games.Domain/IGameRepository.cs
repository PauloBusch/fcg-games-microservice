namespace FCG.Games.Domain;

public interface IGameRepository
{
    Task<Game?> GetByKeyAsync(Guid key, CancellationToken ct);

    Task<bool> ExistByTitleAsync(
        string title,
        Guid? ignoreKey = null,
        CancellationToken ct = default
    );

    Task IndexAsync(Game game, CancellationToken ct);
    
    Task<IEnumerable<EvaluationDto>> GetEvaluationsByGameKeyAsync(Guid gameKey, CancellationToken ct);
    
    Task<DownloadDto?> GetDownloadByGameKeyAsync(Guid gameKey, CancellationToken ct);
    
    Task UpdateAsync(Game game, CancellationToken ct);

    Task<IEnumerable<Game>> GetByTitleAsync(Guid catalogKey, string title, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(Guid catalogKey, Guid gameKey, CancellationToken ct);


}
