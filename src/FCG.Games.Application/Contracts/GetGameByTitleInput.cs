namespace FCG.Games.Application.Contracts;
public record GetGameByTitleInput(
    Guid CatalogKey,
    string Title
 ) : IRequest<GetGameOutput>;
