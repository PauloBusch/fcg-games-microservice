namespace FCG.Games.Api.Mappings;

[Mapper]
public static partial class GamesMapping
{
    public static partial GetGameResponse ToResponse(this GetGameOutput output);

    public static partial CreateGameResponse ToResponse(this CreateGameOutput output);
    
    public static partial GetGameEvaluationsResponse ToResponse(this GetGameEvaluationsOutput output);
    
    public static partial GetGameDownloadResponse ToResponse(this GetGameDownloadOutput output);
    
    public static partial UpdateGameResponse ToResponse(this UpdateGameOutput output);
}
