using FCG.Games.Application.Contracts;
using FCG.Games.Application.Validators;
using FCG.Games.UnitTests.Factories;

namespace FCG.Games.UnitTests.Validators;

public class GetGameEvaluationsInputValidatorTests(FcgFixture fixture)
    : ValidatorTestBase<GetGameEvaluationsInputValidator>(fixture)
{
    [Fact]
    public async Task ShouldAcceptInputAsync()
    {
        var input = ModelFactory.GetGameEvaluationsInput;
        
        var result = await Validator.ValidateAsync(input);
     
        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [MemberData(nameof(InvalidInputs))]
    public async Task ShouldRejectInputAsync(GetGameEvaluationsInput input)
    {
        var result = await Validator.ValidateAsync(input);

        result.IsValid.ShouldBeFalse();
    }

    public static TheoryData<GetGameEvaluationsInput> InvalidInputs()
    {
        var input = ModelFactory.GetGameEvaluationsInput;

        return
        [
            input with { GameKey = Guid.Empty }
        ];
    }
}

