namespace FCG.Games.Api.Contracts;

public record CreateGameRequest(
    Guid? Key,
    string Title,
    string Description,
    CatalogModel Catalog
);