namespace FCG.Games.Application.Contracts;

public record UpdateGameInput(
    Guid CatalogKey,
    Guid GameKey,
    string Title,
    string Description
) : IUseCaseInput<UpdateGameOutput>;
