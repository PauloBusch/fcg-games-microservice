namespace FCG.Games.Api.Contracts;

public record GetGameEvaluationsResponse(
    IEnumerable<EvaluationModel> Evaluations
);

public record EvaluationModel(
    Guid Key,
    int Rating,
    string Comment,
    DateTime CreatedAt,
    string UserName
);
