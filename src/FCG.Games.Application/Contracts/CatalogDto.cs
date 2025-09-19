namespace FCG.Games.Application.Contracts;

public record CatalogDto(
    Guid Key,
    string Name
)
{
    public CatalogDto(Catalog catalog)
        : this(catalog.Key, catalog.Name) { }

    public static implicit operator Catalog(CatalogDto model)
        => new(model.Key, model.Name);
}