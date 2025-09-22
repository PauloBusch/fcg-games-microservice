namespace FCG.Games.Application.Contracts;

public record GetGameInput(
    Guid Key
) : IUseCaseInput<GetGameOutput>;