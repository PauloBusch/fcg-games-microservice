namespace FCG.Games.Application.UseCases;

public class UpdateGameUseCase(IGameRepository gameRepository) : IUseCase<UpdateGameInput, UpdateGameOutput>
{
    public async Task<UpdateGameOutput> Handle(UpdateGameInput input, CancellationToken ct)
    {
        var game = await gameRepository.GetByKeyAsync(input.GameKey, ct: ct);

        if (game is null) throw new FcgNotFoundException(input.GameKey, nameof(game), $"Game with key '{input.GameKey}' was not found.");

        var duplicatedGame = await gameRepository.ExistByTitleAsync(input.Title, ct: ct);

        if (duplicatedGame && game.Title != input.Title) throw new FcgDuplicateException(nameof(Game), $"The title '{input.Title}' is already in use.");

        var updatedGame = new Game(
            input.GameKey,
            input.Title,
            input.Description,
            game.Catalog
        );

        await gameRepository.UpdateAsync(updatedGame, ct);

        return new UpdateGameOutput(
            updatedGame.Key,
            updatedGame.Title,
            updatedGame.Description,
            new CatalogDto(updatedGame.Catalog)
        );
    }
}
