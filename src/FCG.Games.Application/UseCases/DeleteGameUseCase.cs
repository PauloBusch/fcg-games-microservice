namespace FCG.Games.Application.UseCases;
public class DeleteGameUseCase : IRequestHandler<DeleteGameInput, DeleteGameOutput>
{
    private readonly IGameRepository _repository;

    public DeleteGameUseCase(IGameRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteGameOutput> Handle(DeleteGameInput input, CancellationToken ct)
    {
        var deleted = await _repository.DeleteAsync(input.CatalogKey, input.GameKey, ct);
        return deleted ? DeleteGameOutput.Ok() : DeleteGameOutput.Failed();
    }
}
