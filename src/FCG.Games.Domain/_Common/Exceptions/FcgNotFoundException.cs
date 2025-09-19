namespace FCG.Games.Domain._Common.Exceptions;

public class FcgNotFoundException(
    Guid key,
    string entity,
    string message
) : Exception(message)
{
    public Guid Key { get; } = key;
    public string Entity { get; } = entity;
}
