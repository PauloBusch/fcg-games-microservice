namespace FCG.Games.Domain._Common.Exceptions;

public class FcgDuplicateException(
    Guid? key,
    string entity,
    string message
) : Exception(message)
{
    public FcgDuplicateException(
        string entity,
        string message
    ) : this(null, entity, message) { }
    
    public Guid? Key { get; } = key;
    public string Entity { get; } = entity;
}
