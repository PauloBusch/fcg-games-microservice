using FCG.Games.UnitTests.Factories;

namespace FCG.Games.UnitTests._Common;

public abstract class TestBase(FcgFixture fixture) : IClassFixture<FcgFixture>
{
    protected CancellationToken CancellationToken { get; private set; } = fixture.CancellationToken;

    protected static ModelFactory ModelFactory { get; private set; } = new();
}
