using App.Core.Commands.Wallets;
using App.Core.Pipeline.Validators.Helpers;
using App.Interfaces.Repositories.Wallets;

using FluentValidation;

namespace App.Core.Pipeline.Validators.Wallets;

public class RefundCoinsValidator : AbstractValidator<RefundCoinsCommand> {
    public RefundCoinsValidator(IWalletRepository walletRepository) {
        RuleFor(a => a.WalletId).EntityRequiredValidator(walletRepository);
    }
}