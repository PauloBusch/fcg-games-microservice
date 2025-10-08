namespace FCG.Games.Api.Mappings;

[Mapper]
public static partial class GamesRequestToUseCaseMapping
{
    public static partial CatalogDto ToUseCase(this CatalogModel source);
}
