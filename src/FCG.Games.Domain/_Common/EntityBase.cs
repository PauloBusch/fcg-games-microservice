namespace FCG.Games.Domain._Common;

public abstract class EntityBase
{
    protected EntityBase()
    {
        Key = Guid.NewGuid();
    }

    protected EntityBase(Guid key)
    {
        Key = key;
    }

    public Guid Key { get; protected set; }
}
