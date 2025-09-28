namespace FCG.Games.Application.Contracts;

public record GetGameDownloadInput(
    Guid CatalogKey,
    Guid GameKey
) : IUseCaseInput<GetGameDownloadOutput>;
