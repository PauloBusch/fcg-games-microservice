using FluentValidation;

namespace FCG.Games.Application.Validators;

public class UpdateGameInputValidator : AbstractValidator<UpdateGameInput>
{
    public UpdateGameInputValidator()
    {
        RuleFor(x => x.CatalogKey)
            .NotEmpty()
            .WithMessage("CatalogKey is required.");

        RuleFor(x => x.GameKey)
            .NotEmpty()
            .WithMessage("GameKey is required.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);
    }
}
