namespace FCG.Games.Application.UseCases;
public class GetGameByTitleUseCase : IRequestHandler<GetGameByTitleInput, GetGameOutput>
{
    private readonly IGameRepository _repository;

    public GetGameByTitleUseCase(IGameRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetGameOutput> Handle(GetGameByTitleInput request, CancellationToken cancellationToken)
    {
        var games = await _repository.GetByTitleAsync(request.CatalogKey, request.Title, cancellationToken);
        var game = games.FirstOrDefault();

        return game is null
            ? GetGameOutput.NotFound()
            : GetGameOutput.FromDomain(game);
    }
}

