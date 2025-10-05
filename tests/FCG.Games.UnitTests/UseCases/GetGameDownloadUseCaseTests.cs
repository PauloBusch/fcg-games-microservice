using FCG.Games.Application.UseCases;

namespace FCG.Games.UnitTests.UseCases;

public class GetGameDownloadUseCaseTests : UseCaseTestBase<GetGameDownloadUseCase>
{
    private IGameRepository _gameRepository;

    public GetGameDownloadUseCaseTests(FcgFixture fixture) : base(fixture)
    {
        _gameRepository = GetMock<IGameRepository>();
    }

    [Fact]
    public async Task ShouldGetGameDownloadAsync()
    {
        var input = ModelFactory.GetGameDownloadInput;
        var game = new Game(
            input.GameKey,
            "Game Title",
            "Game Description",
            new Catalog(Guid.NewGuid(), "Catalog Name")
        );

        var download = new DownloadDto(
            Guid.NewGuid(),
            "https://example.com/game.zip",
            1024000,
            "1.0.0",
            DateTime.UtcNow
        );

        _gameRepository.GetByKeyAsync(input.GameKey, ct: CancellationToken)
            .Returns(game);

        _gameRepository.GetDownloadByGameKeyAsync(input.GameKey, ct: CancellationToken)
            .Returns(download);

        var output = await UseCase.Handle(input, CancellationToken);

        output.ShouldNotBeNull();
        output.Download.ShouldNotBeNull();
        output.Download.Key.ShouldBe(download.Key);
        output.Download.CreatedAt.ShouldBe(download.CreatedAt);

        await _gameRepository
            .Received(1)
            .GetByKeyAsync(input.GameKey, ct: CancellationToken);

        await _gameRepository
            .Received(1)
            .GetDownloadByGameKeyAsync(input.GameKey, ct: CancellationToken);
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenGameNotFoundAsync()
    {
        var input = ModelFactory.GetGameDownloadInput;

        _gameRepository.GetByKeyAsync(input.GameKey, ct: CancellationToken)
            .Returns((Game?)null);

        var notFoundException = await Should.ThrowAsync<FcgNotFoundException>(
            () => UseCase.Handle(input, CancellationToken)
        );

        notFoundException.Message
            .ShouldBe($"Game with key '{input.GameKey}' was not found.");

        await _gameRepository
            .DidNotReceive()
            .GetDownloadByGameKeyAsync(
                Arg.Any<Guid>(),
                ct: CancellationToken
            );
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenDownloadNotFoundAsync()
    {
        var input = ModelFactory.GetGameDownloadInput;
        var game = new Game(
            input.GameKey,
            "Game Title",
            "Game Description",
            new Catalog(Guid.NewGuid(), "Catalog Name")
        );

        _gameRepository.GetByKeyAsync(input.GameKey, ct: CancellationToken)
            .Returns(game);

        _gameRepository.GetDownloadByGameKeyAsync(input.GameKey, ct: CancellationToken)
            .Returns((DownloadDto?)null);

        var notFoundException = await Should.ThrowAsync<FcgNotFoundException>(
            () => UseCase.Handle(input, CancellationToken)
        );

        notFoundException.Message
            .ShouldBe($"Download for game with key '{input.GameKey}' was not found.");
    }
}
