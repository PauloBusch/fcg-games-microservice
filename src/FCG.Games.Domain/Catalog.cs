namespace FCG.Games.Domain;

public class Catalog : EntityBase
{
    public Catalog(
        Guid key,
        string name
    ) : base(key)
    {
        Name = name;
    }

    public string Name { get; private set; }
}
