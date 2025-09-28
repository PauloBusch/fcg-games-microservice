using FluentValidation;

namespace FCG.Games.Application.Validators;

public class GetGameEvaluationsInputValidator : AbstractValidator<GetGameEvaluationsInput>
{
    public GetGameEvaluationsInputValidator()
    {
        RuleFor(x => x.CatalogKey)
            .NotEmpty()
            .WithMessage("CatalogKey is required.");

        RuleFor(x => x.GameKey)
            .NotEmpty()
            .WithMessage("GameKey is required.");
    }
}
