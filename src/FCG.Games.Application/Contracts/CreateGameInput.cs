namespace FCG.Games.Application.Contracts;

public record CreateGameInput(
    Guid? Key,
    string Title,
    string Description,
    CatalogDto Catalog
) : IUseCaseInput<CreateGameOutput>;