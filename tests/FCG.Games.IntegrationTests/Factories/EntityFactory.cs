using FCG.Games.Domain;

namespace FCG.Games.IntegrationTests.Factories;

public class EntityFactory
{
    public Game Game => new(
       Guid.NewGuid(),
       "Game Title",
       "Game Description",
       Catalog
    );

    private Catalog Catalog => new(
        Guid.NewGuid(),
        "Catalog Name"
    );
}
