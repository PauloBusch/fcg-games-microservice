namespace FCG.Games.Api.Mappings;

[Mapper]
public static partial class GamesRequestToUseCaseMapping
{
    public static partial CreateGameInput ToUseCase(this CreateGameRequest request);
}
