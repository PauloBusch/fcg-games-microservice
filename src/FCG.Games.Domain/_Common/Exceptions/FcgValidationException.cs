namespace FCG.Games.Domain._Common.Exceptions;

public class FcgValidationException(
    string field,
    string message
) : FcgException(message)
{
    public string Field { get; } = field;
}
