using Elastic.Clients.Elasticsearch.SnapshotLifecycleManagement;

namespace FCG.Games.Api.Mappings;

[Mapper]
public static partial class GamesRequestToUseCaseMapping
{
    public static partial CreateGameInput ToUseCase(this CreateGameRequest request);

    public static GetGameByTitleInput ToUseCase(Guid catalogKey, string title) =>
        new GetGameByTitleInput(catalogKey, title);

    public static GetGameEvaluationsInput ToUseCase(Guid catalogKey, Guid gameKey) =>
        new GetGameEvaluationsInput(catalogKey, gameKey);
    
    public static GetGameDownloadInput ToDownloadUseCase(Guid catalogKey, Guid gameKey) =>
        new GetGameDownloadInput(catalogKey, gameKey);
    
    public static UpdateGameInput ToUseCase(this UpdateGameRequest request, Guid catalogKey, Guid gameKey) =>
        new UpdateGameInput(catalogKey, gameKey, request.Title, request.Description);

    public static DeleteGameInput ToDeleteGameUseCase(Guid catalogKey, Guid gameKey) =>
             new DeleteGameInput(catalogKey, gameKey);

    public static CreateEvaluationInput ToUseCase(Guid catalogKey, Guid gameKey, CreateEvaluationRequest request) =>
        new CreateEvaluationInput(catalogKey, gameKey, request.Reviewer, request.Rating, request.Comment);

}
