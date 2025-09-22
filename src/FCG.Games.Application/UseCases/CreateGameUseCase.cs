namespace FCG.Games.Application.UseCases;

public class CreateGameUseCase(IGameRepository gameRepository) : IUseCase<CreateGameInput, CreateGameOutput>
{
    public async Task<CreateGameOutput> Handle(CreateGameInput input, CancellationToken ct)
    {
        var duplicatedGame = await gameRepository.ExistByTitleAsync(input.Title, ct: ct);

        if (duplicatedGame) throw new FcgDuplicateException(nameof(Game), $"The title '{input.Title}' is already in use.");

        var game = new Game(
            input.Key,
            input.Title,
            input.Description,
            input.Catalog
        );

        await gameRepository.IndexAsync(game, ct);

        return new CreateGameOutput(game);
    }
}
