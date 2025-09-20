using FCG.Games.Application.Contracts;
using FCG.Games.UnitTests.Factories;

namespace FCG.Games.UnitTests.Validators;

public class CreateGameInputValidatorTests(FcgFixture fixture)
    : ValidatorTestBase<CreateGameInputValidator>(fixture)
{
    [Fact]
    public async Task ShouldAcceptInputAsync()
    {
        var input = ModelFactory.CreateGameInput;
        
        var result = await Validator.ValidateAsync(input);
     
        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [MemberData(nameof(InvalidInputs))]
    public async Task ShouldRejectInputAsync(CreateGameInput input)
    {
        var result = await Validator.ValidateAsync(input);

        result.IsValid.ShouldBeFalse();
    }

    public static TheoryData<CreateGameInput> InvalidInputs()
    {
        var input = ModelFactory.CreateGameInput;

        return
        [
            input with { Title = default },
            input with { Title = string.Empty },
            input with { Title = new string('T', 101) },
            input with { Description = default },
            input with { Description = string.Empty },
            input with { Description = new string('D', 501) },
            input with { Catalog = default },
            input with {
                Catalog = input.Catalog
                    with { Key = default }
            },
            input with {
                Catalog = input.Catalog
                    with { Name = default }
            },
            input with {
                Catalog = input.Catalog
                    with { Name = string.Empty }
            },
            input with {
                Catalog = input.Catalog
                    with { Name = new string('C', 101) }
            },
        ];
    }
}
