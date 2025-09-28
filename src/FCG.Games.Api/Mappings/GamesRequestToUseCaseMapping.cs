namespace FCG.Games.Api.Mappings;

[Mapper]
public static partial class GamesRequestToUseCaseMapping
{
    public static partial CreateGameInput ToUseCase(this CreateGameRequest request);
    
    public static GetGameEvaluationsInput ToUseCase(Guid catalogKey, Guid gameKey) =>
        new GetGameEvaluationsInput(catalogKey, gameKey);
    
    public static GetGameDownloadInput ToDownloadUseCase(Guid catalogKey, Guid gameKey) =>
        new GetGameDownloadInput(catalogKey, gameKey);
    
    public static UpdateGameInput ToUseCase(this UpdateGameRequest request, Guid catalogKey, Guid gameKey) =>
        new UpdateGameInput(catalogKey, gameKey, request.Title, request.Description);
}
