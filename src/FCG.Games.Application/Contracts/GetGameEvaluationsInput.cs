namespace FCG.Games.Application.Contracts;

public record GetGameEvaluationsInput(
    Guid CatalogKey,
    Guid GameKey
) : IUseCaseInput<GetGameEvaluationsOutput>;
