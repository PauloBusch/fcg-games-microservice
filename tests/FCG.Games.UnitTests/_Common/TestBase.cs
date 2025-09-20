using FCG.Games.UnitTests.Factories;

namespace FCG.Games.UnitTests._Common;

public abstract class TestBase(FcgFixture fixture) : IClassFixture<FcgFixture>
{
    protected CancellationToken CancellationToken { get; } = fixture.CancellationToken;

    protected static ModelFactory ModelFactory { get; } = new();
}
