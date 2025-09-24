namespace FCG.Games.IntegrationTests.Controllers;

public class GamesControllerTests : ControllerTestBase
{
    private IGameRepository _gameRepository;

    public GamesControllerTests(FcgFixture fixture) : base(fixture, "games")
    {
        _gameRepository = GetService<IGameRepository>();
    }

    [Fact]
    public async Task ShouldCreateGameAsync()
    {
        var request = ModelFactory.CreateGameRequest
            with { Key = default };

        var (httpMessage, response) = await Requester.PostAsync<CreateGameResponse>(Uri, request, CancellationToken);

        httpMessage.StatusCode
            .ShouldBe(HttpStatusCode.Created);

        response.ShouldNotBeNull();
        response.Key.ShouldNotBe(Guid.Empty);
        response.Title.ShouldBe(request.Title);
        response.Description.ShouldBe(request.Description);
        
        var game = await _gameRepository
            .GetByKeyAsync(response.Key, CancellationToken);
        
        game.ShouldNotBeNull();
        game!.Key.ShouldBe(response.Key);
        game.Title.ShouldBe(response.Title);
        game.Description.ShouldBe(response.Description);
    }

    [Fact]
    public async Task ShouldRejectRequestAsync()
    {
        var request = ModelFactory.CreateGameRequest
            with { Title = string.Empty };

        var (httpMessage, response) = await Requester
            .PostAsync<ErrorResponse>(Uri, request, CancellationToken);

        httpMessage.StatusCode
            .ShouldBe(HttpStatusCode.BadRequest);
        response
            .ShouldNotBeNull();
        response.Errors
            .ShouldNotBeEmpty();

        var game = await _gameRepository
            .GetByKeyAsync(request.Key.Value, CancellationToken);
        game
            .ShouldBeNull();
    }

    [Fact]
    public async Task ShouldGetGameAsync()
    {
        var game = EntityFactory.Game;

        await _gameRepository.IndexAsync(game, CancellationToken);

        var (getHttpMessage, getResponse) = await Requester.GetAsync<CreateGameResponse>(
            new Uri($"{Uri}/{game.Key}"),
            ct: CancellationToken
        );

        getHttpMessage.StatusCode
            .ShouldBe(HttpStatusCode.OK);
        getResponse
            .ShouldNotBeNull();
        getResponse.Key
            .ShouldBe(game.Key);
        getResponse.Title
            .ShouldBe(game.Title);
        getResponse.Description
            .ShouldBe(game.Description);
    }

    [Fact]
    public async Task ShouldReturnNotFoundForMissingGameAsync()
    {
        var missingKey = Guid.NewGuid();
        var getUri = new Uri($"{Uri}/{missingKey}");

        var (httpMessage, response) = await Requester.GetAsync<ErrorResponse>(getUri, null, CancellationToken);

        httpMessage.StatusCode
            .ShouldBe(HttpStatusCode.NotFound);        
        response
            .ShouldNotBeNull();
        response.Key
            .ShouldBe(missingKey);
        response.Message
            .ShouldBe($"Game with key '{missingKey}' was not found.");
    }
}
