using FCG.Games.Application.Contracts;
using FCG.Games.Application.Validators;
using FCG.Games.UnitTests.Factories;

namespace FCG.Games.UnitTests.Validators;

public class UpdateGameInputValidatorTests(FcgFixture fixture)
    : ValidatorTestBase<UpdateGameInputValidator>(fixture)
{
    [Fact]
    public async Task ShouldAcceptInputAsync()
    {
        var input = ModelFactory.UpdateGameInput;
        
        var result = await Validator.ValidateAsync(input);
     
        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [MemberData(nameof(InvalidInputs))]
    public async Task ShouldRejectInputAsync(UpdateGameInput input)
    {
        var result = await Validator.ValidateAsync(input);

        result.IsValid.ShouldBeFalse();
    }

    public static TheoryData<UpdateGameInput> InvalidInputs()
    {
        var input = ModelFactory.UpdateGameInput;

        return
        [
            input with { GameKey = Guid.Empty },
            input with { Title = default },
            input with { Title = string.Empty },
            input with { Title = new string('T', 101) },
            input with { Description = default },
            input with { Description = string.Empty },
            input with { Description = new string('D', 501) }
        ];
    }
}

