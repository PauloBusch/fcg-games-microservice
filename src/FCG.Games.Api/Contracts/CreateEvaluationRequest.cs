namespace FCG.Games.Api.Contracts;

public class CreateEvaluationRequest
{
    public string Reviewer { get; set; }
    public int Rating { get; set; } 
    public string? Comment { get; set; }
}