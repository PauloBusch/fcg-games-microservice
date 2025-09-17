namespace FCG.Games.Application.Contracts;

public record CreateGameResponse(
    Guid Key,
    string Title,
    string Description,
    CatalogModel Catalog
);