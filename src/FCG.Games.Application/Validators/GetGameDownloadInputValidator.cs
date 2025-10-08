using FluentValidation;

namespace FCG.Games.Application.Validators;

public class GetGameDownloadInputValidator : AbstractValidator<GetGameDownloadInput>
{
    public GetGameDownloadInputValidator()
    {
        RuleFor(x => x.GameKey)
            .NotEmpty();
    }
}
