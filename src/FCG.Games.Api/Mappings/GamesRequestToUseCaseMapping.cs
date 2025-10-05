namespace FCG.Games.Api.Mappings;

[Mapper]
public static partial class GamesRequestToUseCaseMapping
{
    public static partial CreateGameInput ToUseCase(this CreateGameRequest request);
    
    public static GetGameEvaluationsInput ToUseCase(Guid gameKey) =>
        new GetGameEvaluationsInput(gameKey);
    
    public static GetGameDownloadInput ToDownloadUseCase(Guid gameKey) =>
        new GetGameDownloadInput(gameKey);
    
    public static UpdateGameInput ToUseCase(this UpdateGameRequest request, Guid gameKey) =>
        new UpdateGameInput(gameKey, request.Title, request.Description);
}
