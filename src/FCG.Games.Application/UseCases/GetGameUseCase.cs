namespace FCG.Games.Application.UseCases;

public class GetGameUseCase(IGameRepository gameRepository) : IUseCase<GetGameInput, GetGameOutput>
{
    public async Task<GetGameOutput> Handle(GetGameInput input, CancellationToken ct)
    {
        var game = await gameRepository.GetByKeyAsync(input.Key, ct: ct);

        if (game is null) throw new FcgNotFoundException(input.Key, nameof(game), $"Game with key '{input.Key}' was not found.");

        return new GetGameOutput(game);
    }
}
