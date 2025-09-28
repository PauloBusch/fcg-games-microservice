namespace FCG.Games.Application.UseCases;

public class GetGameEvaluationsUseCase(IGameRepository gameRepository) : IUseCase<GetGameEvaluationsInput, GetGameEvaluationsOutput>
{
    public async Task<GetGameEvaluationsOutput> Handle(GetGameEvaluationsInput input, CancellationToken ct)
    {
        var game = await gameRepository.GetByKeyAsync(input.GameKey, ct: ct);

        if (game is null) throw new FcgNotFoundException(input.GameKey, nameof(game), $"Game with key '{input.GameKey}' was not found.");

        var evaluations = await gameRepository.GetEvaluationsByGameKeyAsync(input.GameKey, ct: ct);

        return new GetGameEvaluationsOutput(evaluations);
    }
}
