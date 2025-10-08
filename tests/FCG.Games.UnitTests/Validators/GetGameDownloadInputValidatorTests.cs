using FCG.Games.Application.Contracts;
using FCG.Games.Application.Validators;
using FCG.Games.UnitTests.Factories;

namespace FCG.Games.UnitTests.Validators;

public class GetGameDownloadInputValidatorTests(FcgFixture fixture)
    : ValidatorTestBase<GetGameDownloadInputValidator>(fixture)
{
    [Fact]
    public async Task ShouldAcceptInputAsync()
    {
        var input = ModelFactory.GetGameDownloadInput;
        
        var result = await Validator.ValidateAsync(input);
     
        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [MemberData(nameof(InvalidInputs))]
    public async Task ShouldRejectInputAsync(GetGameDownloadInput input)
    {
        var result = await Validator.ValidateAsync(input);

        result.IsValid.ShouldBeFalse();
    }

    public static TheoryData<GetGameDownloadInput> InvalidInputs()
    {
        var input = ModelFactory.GetGameDownloadInput;

        return
        [
            input with { GameKey = Guid.Empty }
        ];
    }
}

