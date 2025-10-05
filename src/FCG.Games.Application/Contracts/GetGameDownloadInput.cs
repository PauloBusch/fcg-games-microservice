namespace FCG.Games.Application.Contracts;

public record GetGameDownloadInput(
    Guid GameKey
) : IUseCaseInput<GetGameDownloadOutput>;
