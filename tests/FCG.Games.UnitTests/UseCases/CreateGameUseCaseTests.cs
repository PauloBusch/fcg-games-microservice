using FCG.Games.Application.UseCases;

namespace FCG.Games.UnitTests.UseCases;

public class CreateGameUseCaseTests : UseCaseTestBase<CreateGameUseCase>
{
    private IGameRepository _gameRepository;

    public CreateGameUseCaseTests(FcgFixture fixture) : base(fixture)
    {
        _gameRepository = GetMock<IGameRepository>();
    }

    [Fact]
    public async Task ShouldCreateGameAsync()
    {
        var input = ModelFactory.CreateGameInput;

        var output = await UseCase.Handle(input, CancellationToken);

        output.ShouldNotBeNull();
        output.Key.ShouldNotBe(Guid.Empty);
        output.Title.ShouldBe(input.Title);
        output.Description.ShouldBe(input.Description);

        await _gameRepository
            .Received(1)
            .IndexAsync(
                Arg.Any<Game>(),
                CancellationToken
            );
    }

    [Fact]
    public async Task ShouldThrowExceptionForGameDuplicationAsync()
    {
        var input = ModelFactory.CreateGameInput;

        _gameRepository.ExistByTitleAsync(input.Title, ct: CancellationToken)
            .Returns(true);

        var duplicateException = await Should.ThrowAsync<FcgDuplicateException>(
            () => UseCase.Handle(input, CancellationToken)
         );

        duplicateException.Message
            .ShouldBe($"The title '{input.Title}' is already in use.");
        await _gameRepository
            .DidNotReceive()
            .IndexAsync(
                Arg.Any<Game>(),
                CancellationToken
            );
    }
}
