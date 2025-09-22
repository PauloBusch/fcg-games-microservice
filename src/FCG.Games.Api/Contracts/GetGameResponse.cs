namespace FCG.Games.Api.Contracts;

public record GetGameResponse(
    Guid Key,
    string Title,
    string Description,
    CatalogModel Catalog
);