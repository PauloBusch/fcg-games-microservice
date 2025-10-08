namespace FCG.Games.Application.UseCases;

public class GetGameDownloadUseCase(IGameRepository gameRepository) : IUseCase<GetGameDownloadInput, GetGameDownloadOutput>
{
    public async Task<GetGameDownloadOutput> Handle(GetGameDownloadInput input, CancellationToken ct)
    {
        var game = await gameRepository.GetByKeyAsync(input.GameKey, ct: ct);

        if (game is null) throw new FcgNotFoundException(input.GameKey, nameof(game), $"Game with key '{input.GameKey}' was not found.");

        var download = await gameRepository.GetDownloadByGameKeyAsync(input.GameKey, ct: ct);

        if (download is null) throw new FcgNotFoundException(input.GameKey, nameof(download), $"Download for game with key '{input.GameKey}' was not found.");

        return new GetGameDownloadOutput(download);
    }
}
