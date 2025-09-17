using FCG.Games.Application.Contracts;
using FCG.Games.Domain;
using FluentValidation;

namespace FCG.Games.Application.Validators;

public class CreateGameInputValidator : AbstractValidator<CreateGameRequest>
{
    public CreateGameInputValidator(IGameRepository gameRepository)
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100)
            .MustAsync((r, _, ct) => gameRepository.ExistByTitleAsync(r.Title, r.Key, ct))
            .WithMessage(r => $"The title '{r.Title}' is already in use.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Catalog)
            .NotNull()
            .ChildRules(ValidateCatalog);
    }

    private static void ValidateCatalog(InlineValidator<CatalogModel> validator)
    {
        validator.RuleFor(c => c.Key)
            .NotEmpty();

        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}
