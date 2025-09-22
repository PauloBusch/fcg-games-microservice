namespace FCG.Games.Api.Mappings;

[Mapper]
public static partial class GamesMapping
{
    public static partial GetGameResponse ToResponse(this GetGameOutput output);

    public static partial CreateGameResponse ToResponse(this CreateGameOutput output);
}
