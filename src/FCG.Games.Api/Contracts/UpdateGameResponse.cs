namespace FCG.Games.Api.Contracts;

public record UpdateGameResponse(
    Guid Key,
    string Title,
    string Description,
    CatalogModel Catalog
);
