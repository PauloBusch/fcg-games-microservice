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

    public bool IsNotFound { get; set; }

    internal static GetGameOutput FromDomain(Game game)
    {
        return new GetGameOutput(game)
        {
            IsNotFound = false
        };
    }

    internal static GetGameOutput NotFound()
    {
        return new GetGameOutput(
            Guid.Empty,
            string.Empty,
            string.Empty,
            new CatalogDto(Guid.Empty, string.Empty)
        )
        {
            IsNotFound = true
        };
    }
};