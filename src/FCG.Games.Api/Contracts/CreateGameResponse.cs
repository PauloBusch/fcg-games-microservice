namespace FCG.Games.Api.Contracts;

public record CreateGameResponse(
    Guid Key,
    string Title,
    string Description,
    CatalogModel Catalog
);