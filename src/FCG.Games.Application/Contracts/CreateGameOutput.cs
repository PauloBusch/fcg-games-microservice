namespace FCG.Games.Application.Contracts;

public record CreateGameOutput(
    Guid Key,
    string Title,
    string Description,
    CatalogDto Catalog
);