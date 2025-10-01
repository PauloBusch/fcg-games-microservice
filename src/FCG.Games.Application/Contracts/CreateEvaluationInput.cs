namespace FCG.Games.Application.Contracts;
public class CreateEvaluationInput : IRequest<CreateEvaluationOutput>
{
    public CreateEvaluationInput(Guid catalogKey, Guid gameKey, string reviewer, int rating, string comment)
    {
        CatalogKey = catalogKey;
        GameKey = gameKey;
        Reviewer = reviewer;
        Rating = rating;
        Comment = comment;
    }

    public Guid CatalogKey { get; }
    public Guid GameKey { get; }
    public string Reviewer { get; }
    public int Rating { get; }
    public string Comment { get; }
}