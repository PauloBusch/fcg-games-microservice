namespace FCG.Games.Application.Contracts;
public class CreateEvaluationOutput
{
    public Guid EvaluationId { get; init; }
    public DateTime CreatedAt { get; init; }

    public bool IsNotCreated => EvaluationId == Guid.Empty;
}