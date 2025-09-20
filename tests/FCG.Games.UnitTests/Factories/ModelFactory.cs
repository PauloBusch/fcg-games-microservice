using FCG.Games.Application.Contracts;

namespace FCG.Games.UnitTests.Factories;

public class ModelFactory
{
    public CreateGameInput CreateGameInput => new(
       Guid.NewGuid(),
       "Game Title",
       "Game Description",
       CatalogDto
    );

    public CatalogDto CatalogDto => new(
        Guid.NewGuid(),
        "Catalog Name"
    );
}
