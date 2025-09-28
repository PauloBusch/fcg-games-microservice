namespace FCG.Games.Domain;

public record EvaluationDto(
    Guid Key,
    int Rating,
    string Comment,
    DateTime CreatedAt,
    string UserName
);
