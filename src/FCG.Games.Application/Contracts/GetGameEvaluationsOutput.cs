using FCG.Games.Domain;

namespace FCG.Games.Application.Contracts;

public record GetGameEvaluationsOutput(
    IEnumerable<EvaluationDto> Evaluations
);
