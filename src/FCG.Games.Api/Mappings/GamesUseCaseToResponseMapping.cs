namespace FCG.Games.Api.Mappings;

[Mapper]
public static partial class GamesMapping
{
    public static partial CreateGameResponse ToResponse(this CreateGameOutput output);
}
