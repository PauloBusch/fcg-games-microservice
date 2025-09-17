namespace FCG.Games.Application.Contracts;

public record CreateGameRequest(
    Guid Key,
    string Title,
    string Description,
    CatalogModel Catalog
);