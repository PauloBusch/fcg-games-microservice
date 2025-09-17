namespace FCG.Games.Application.UserCases;

public class CreateGameUseCase
{
    private readonly IGameRepository _gameRepository;

    public CreateGameUseCase(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<CreateGameResponse> ExecuteAsync(CreateGameRequest input, CancellationToken ct)
    {
        var game = new Game(
            input.Key,
            input.Title,
            input.Description,
            input.Catalog
        );

        await _gameRepository.IndexAsync(game, ct);

        return new CreateGameResponse(
            game.Key,
            game.Title,
            game.Description,
            new CatalogModel(game.Catalog)
        );
    }
}
