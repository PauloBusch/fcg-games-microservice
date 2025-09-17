namespace FCG.Games.Domain;

public interface IGameRepository
{
    Task IndexAsync(Game game, CancellationToken ct);

    Task<bool> ExistByTitleAsync(
        string title,
        Guid? ignoreKey = null,
        CancellationToken ct = default
    );
}
