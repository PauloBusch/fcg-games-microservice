using FCG.Games.Domain;

namespace FCG.Games.Application.Contracts;

public record CatalogModel(
    Guid Key,
    string Name
)
{
    public CatalogModel(Catalog catalog)
        : this(catalog.Key, catalog.Name) { }

    public static implicit operator Catalog(CatalogModel model)
        => new(model.Key, model.Name);
}