using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace FCG.Games.IntegrationTests;

public class FcgFixture : IClassFixture<WebApplicationFactory<Program>>, IDisposable
{
    public HttpClient Client { get; }

    public WebApplicationFactory<Program> Factory { get; }

    public CancellationToken CancellationToken { get; }

    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public FcgFixture(WebApplicationFactory<Program> factory)
    {
        CancellationToken = _cancellationTokenSource.Token;
        Factory = factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("TestAutomation");
        });

        Client = Factory.CreateClient();
    }

    public void Dispose()
    {
        _cancellationTokenSource.Cancel();
        Client.Dispose();
        Factory.Dispose();
    }
}
