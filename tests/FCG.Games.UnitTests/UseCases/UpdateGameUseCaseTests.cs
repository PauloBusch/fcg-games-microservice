using FCG.Games.Application.UseCases;

namespace FCG.Games.UnitTests.UseCases;

public class UpdateGameUseCaseTests : UseCaseTestBase<UpdateGameUseCase>
{
    private IGameRepository _gameRepository;

    public UpdateGameUseCaseTests(FcgFixture fixture) : base(fixture)
    {
        _gameRepository = GetMock<IGameRepository>();
    }

    [Fact]
    public async Task ShouldUpdateGameAsync()
    {
        var input = ModelFactory.UpdateGameInput;
        var existingGame = new Game(
            input.GameKey,
            "Old Title",
            "Old Description",
            new Catalog(Guid.NewGuid(), "Catalog Name")
        );

        _gameRepository.GetByKeyAsync(input.GameKey, ct: CancellationToken)
            .Returns(existingGame);

        _gameRepository.ExistByTitleAsync(input.Title, ct: CancellationToken)
            .Returns(false);

        var output = await UseCase.Handle(input, CancellationToken);

        output.ShouldNotBeNull();
        output.Key.ShouldBe(input.GameKey);
        output.Title.ShouldBe(input.Title);
        output.Description.ShouldBe(input.Description);

        await _gameRepository
            .Received(1)
            .UpdateAsync(
                Arg.Is<Game>(g => 
                    g.Key == input.GameKey && 
                    g.Title == input.Title && 
                    g.Description == input.Description),
                CancellationToken
            );
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenGameNotFoundAsync()
    {
        var input = ModelFactory.UpdateGameInput;

        _gameRepository.GetByKeyAsync(input.GameKey, ct: CancellationToken)
            .Returns((Game?)null);

        var notFoundException = await Should.ThrowAsync<FcgNotFoundException>(
            () => UseCase.Handle(input, CancellationToken)
        );

        notFoundException.Message
            .ShouldBe($"Game with key '{input.GameKey}' was not found.");

        await _gameRepository
            .DidNotReceive()
            .UpdateAsync(
                Arg.Any<Game>(),
                CancellationToken
            );
    }

    [Fact]
    public async Task ShouldThrowExceptionForGameDuplicationAsync()
    {
        var input = ModelFactory.UpdateGameInput;
        var existingGame = new Game(
            input.GameKey,
            "Old Title",
            "Old Description",
            new Catalog(Guid.NewGuid(), "Catalog Name")
        );

        _gameRepository.GetByKeyAsync(input.GameKey, ct: CancellationToken)
            .Returns(existingGame);

        _gameRepository.ExistByTitleAsync(input.Title, ct: CancellationToken)
            .Returns(true);

        var duplicateException = await Should.ThrowAsync<FcgDuplicateException>(
            () => UseCase.Handle(input, CancellationToken)
        );

        duplicateException.Message
            .ShouldBe($"The title '{input.Title}' is already in use.");

        await _gameRepository
            .DidNotReceive()
            .UpdateAsync(
                Arg.Any<Game>(),
                CancellationToken
            );
    }
}

