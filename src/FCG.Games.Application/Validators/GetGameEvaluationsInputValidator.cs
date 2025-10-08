using FluentValidation;

namespace FCG.Games.Application.Validators;

public class GetGameEvaluationsInputValidator : AbstractValidator<GetGameEvaluationsInput>
{
    public GetGameEvaluationsInputValidator()
    {
        RuleFor(x => x.GameKey)
            .NotEmpty();
    }
}
