namespace FCG.Games.Application.Contracts;

public record GetGameOutput(
    Guid Key,
    string Title,
    string Description,
    CatalogDto Catalog
)
{
    public GetGameOutput(Game game)
        : this(
            game.Key,
            game.Title,
            game.Description,
            new CatalogDto(game.Catalog)
        )
    { }
};