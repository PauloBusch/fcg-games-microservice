namespace FCG.Games.Domain;

public class Catalog(
    Guid key,
    string name
) : EntityBase(key)
{
    public string Name { get; private set; } = name;
}
