namespace FCG.Games.Api.Mappings;

[Mapper]
public static partial class GamesMapping
{
    public static partial CreateGameInput ToUseCase(this CreateGameRequest request);

    public static partial CreateGameResponse ToResponse(this CreateGameOutput output);
}
