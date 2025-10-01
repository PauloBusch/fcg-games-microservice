namespace FCG.Games.Application.Contracts;
public record DeleteGameInput(Guid CatalogKey, Guid GameKey) : IRequest<DeleteGameOutput>;