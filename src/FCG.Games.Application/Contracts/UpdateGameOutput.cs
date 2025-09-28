namespace FCG.Games.Application.Contracts;

public record UpdateGameOutput(
    Guid Key,
    string Title,
    string Description,
    CatalogDto Catalog
);
