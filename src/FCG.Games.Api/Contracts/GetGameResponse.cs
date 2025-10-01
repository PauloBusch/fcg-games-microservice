namespace FCG.Games.Api.Contracts;

public record GetGameResponse(
    Guid Key,
    string Title,
    string Description,
    CatalogModel Catalog
)
{
    public static GetGameResponse FromOutput(GetGameOutput output)
    {
        return new GetGameResponse(
            output.Key,
            output.Title,
            output.Description,
            new CatalogModel(output.Catalog.Key, output.Catalog.Name)
        );
    }
}