using FCG.Games.IntegrationTests.Factories;
using Microsoft.AspNetCore.TestHost;

namespace FCG.Games.UnitTests;

public class FcgFixture
{

    public HttpClient Client { get; private set; }

    public TestServer Server { get; private set; }

    public EntityFactory EntityFactory { get; private set; }
}
