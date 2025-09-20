using FCG.Games.Api.Contracts;

namespace FCG.Games.UnitTests.Factories;

public class ModelFactory
{
    public CreateGameRequest CreateGameRequest => new(
       Guid.NewGuid(),
       $"Game Title - {Guid.NewGuid()}",
       "Game Description",
       CatalogModel
    );

    public CatalogModel CatalogModel => new(
        Guid.NewGuid(),
        "Catalog Name"
    );
}
