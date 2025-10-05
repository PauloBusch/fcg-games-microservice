using FCG.Games.Application.Contracts;

namespace FCG.Games.UnitTests.Factories;

public class ModelFactory
{
    public CreateGameInput CreateGameInput => new(
       Guid.NewGuid(),
       "Game Title",
       "Game Description example test",
       CatalogDto
    );

    public UpdateGameInput UpdateGameInput => new(
        Guid.NewGuid(),
        "Updated Game Title",
        "Updated Game Descriptin"
    );

    public GetGameEvaluationsInput GetGameEvaluationsInput => new(
        Guid.NewGuid()
    );

    public GetGameDownloadInput GetGameDownloadInput => new(
        Guid.NewGuid()
    );

    private CatalogDto CatalogDto => new(
        Guid.NewGuid(),
        "Catalog Name"
    );
}
