using FluentValidation;

namespace FCG.Games.Application.Validators;

public class CreateGameInputValidator : AbstractValidator<CreateGameInput>
{
    public CreateGameInputValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Catalog)
            .NotNull()
            .ChildRules(ValidateCatalog);
    }

    private static void ValidateCatalog(InlineValidator<CatalogDto> validator)
    {
        validator.RuleFor(c => c.Key)
            .NotEmpty();

        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}
