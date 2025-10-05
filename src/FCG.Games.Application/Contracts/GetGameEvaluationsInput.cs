namespace FCG.Games.Application.Contracts;

public record GetGameEvaluationsInput(
    Guid GameKey
) : IUseCaseInput<GetGameEvaluationsOutput>;
