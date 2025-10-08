using FCG.Games.Application.UseCases;

namespace FCG.Games.UnitTests.UseCases;

public class GetGameEvaluationsUseCaseTests : UseCaseTestBase<GetGameEvaluationsUseCase>
{
    private IGameRepository _gameRepository;

    public GetGameEvaluationsUseCaseTests(FcgFixture fixture) : base(fixture)
    {
        _gameRepository = GetMock<IGameRepository>();
    }

    [Fact]
    public async Task ShouldGetGameEvaluationsAsync()
    {
        var input = ModelFactory.GetGameEvaluationsInput;
        var game = new Game(
            input.GameKey,
            "Game Title",
            "Game Description",
            new Catalog(Guid.NewGuid(), "Catalog Name")
        );

        var evaluations = new List<EvaluationDto>
        {
            new EvaluationDto(
                Guid.NewGuid(),
                5,
                "Great game!",
                DateTime.UtcNow,
                "User1"
            ),
            new EvaluationDto(
                Guid.NewGuid(),
                4,
                "Good game",
                DateTime.UtcNow,
                "User2"
            )
        };

        _gameRepository.GetByKeyAsync(input.GameKey, ct: CancellationToken)
            .Returns(game);

        _gameRepository.GetEvaluationsByGameKeyAsync(input.GameKey, ct: CancellationToken)
            .Returns(evaluations);

        var output = await UseCase.Handle(input, CancellationToken);

        output.ShouldNotBeNull();
        output.Evaluations.ShouldNotBeNull();
        output.Evaluations.ShouldNotBeEmpty();
        output.Evaluations.Count().ShouldBe(2);

        await _gameRepository
            .Received(1)
            .GetByKeyAsync(input.GameKey, ct: CancellationToken);

        await _gameRepository
            .Received(1)
            .GetEvaluationsByGameKeyAsync(input.GameKey, ct: CancellationToken);
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenGameNotFoundAsync()
    {
        var input = ModelFactory.GetGameEvaluationsInput;

        _gameRepository.GetByKeyAsync(input.GameKey, ct: CancellationToken)
            .Returns((Game?)null);

        var notFoundException = await Should.ThrowAsync<FcgNotFoundException>(
            () => UseCase.Handle(input, CancellationToken)
        );

        notFoundException.Message
            .ShouldBe($"Game with key '{input.GameKey}' was not found.");

        await _gameRepository
            .DidNotReceive()
            .GetEvaluationsByGameKeyAsync(
                Arg.Any<Guid>(),
                ct: CancellationToken
            );
    }
}
