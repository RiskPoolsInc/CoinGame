using System.Text.RegularExpressions;

using App.Core.Commands.Wallets;
using App.Core.Pipeline.Validators.Wallets;

using Shouldly;

namespace TS.Tests.Validators;

public class CreateWalletValidators {
    [SetUp]
    public void Setup() {
    }

    [Test]
    public void CreateWalletValidatorTypeIdTest() {
        CreateWalletValidator.CreateInstance().ValidateTypeId(1).ShouldBeTrue();
        CreateWalletValidator.CreateInstance().ValidateTypeId(10).ShouldBeFalse();
    }

    [Test]
    public void CreateWalletValidatorTest() {
        var validator = new CreateWalletValidator();

        var command = new CreateWalletCommand() {
            TypeId = 1,
        };
        var validationResult = validator.Validate(command);
        validationResult.IsValid.ShouldBeTrue();
        command.TypeId = 10;
        validationResult = validator.Validate(command);
        validationResult.IsValid.ShouldBeFalse();
    }

    [Test]
    public void ValidateCorrectWalletPrivateKeyTest() {
        var arg = "1913a27ac39d103f55386b82f39e9c0d46f35d4e23fb0cab07b56819cf960ac7";
        var countChars = arg.Length;
        var pattern = @"\w+|\d+";
        var regex = new Regex(pattern);
        var replaced = regex.Replace(arg, "");
        replaced.Length.ShouldBe(0);
    }

    [Test]
    public void ValidateInCorrectWalletPrivateKeyTest() {
        var arg = "!!1913a27ac39d103f55386b82f39e9c0d46f35d4e23fb0cab07b56819cf960ac7++";
        var countChars = arg.Length;
        var pattern = @"\w+|\d+";
        var regex = new Regex(pattern);
        var replaced = regex.Replace(arg, "");
        replaced.Length.ShouldBeGreaterThan(0);
    }
}