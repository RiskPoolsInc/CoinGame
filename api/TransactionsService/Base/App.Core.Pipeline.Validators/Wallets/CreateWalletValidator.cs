using App.Core.Commands.Wallets;
using App.Core.Enums;
using App.Core.Pipeline.Validators.Helpers;

using FluentValidation;

namespace App.Core.Pipeline.Validators.Wallets;

public class CreateWalletValidator : AbstractValidator<CreateWalletCommand> {
    public CreateWalletValidator() {
        RuleFor(a => a.TypeId)
           .SetValidator(new EnumValidator<WalletTypes>())
           .WithMessage("TypeId is not valid");
    }

    public static CreateWalletValidator CreateInstance() {
        return new CreateWalletValidator();
    }

    public bool ValidateTypeId(int typeId) => Enum.GetValues<WalletTypes>().Contains((WalletTypes)typeId);
}