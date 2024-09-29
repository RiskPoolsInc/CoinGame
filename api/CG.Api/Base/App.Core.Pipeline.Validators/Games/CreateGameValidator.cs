using App.Core.Commands.Games;
using App.Core.Enums;
using App.Core.Pipeline.Validators.Helpers;
using App.Interfaces.Repositories.Wallets;
using App.Services.WalletService;

using FluentValidation;

using Microsoft.EntityFrameworkCore;

namespace App.Core.Pipeline.Validators.Games;

public class CreateGameValidator : AbstractValidator<CreateGameCommand> {
    public CreateGameValidator(IWalletRepository walletRepository, IWalletService walletService) {
        RuleFor(a => a.WalletId).EntityRequiredValidator(walletRepository, "WalletId");

        RuleFor(a => a.WalletId)
           .MustAsync(async (walletId, ct) => {
                var activeGame =
                    await walletRepository.AnyAsync(
                        s => s.Games.Any(a => a.StateId != (int)GameStateTypes.Completed && a.WalletId == walletId), ct);
                return !activeGame;
            })
           .WithMessage("Found active game");
        RuleFor(a => a.Rate).GreaterThanOrEqualTo(10000);

        RuleFor(a => a)
           .MustAsync(async (cmd, ct) => {
                var wallet = await walletRepository.Get(cmd.WalletId).SingleAsync(ct);

                var balanceView = await walletService.GetBalance(wallet.ImportedWalletId);

                // if (balanceView.Balance < cmd.Rate) {
                //     var maxRate = await walletService.TransactionMaxRate(wallet.Hash, wallet.PrivateKey, balanceView.Balance);
                // }

                return true;
                return balanceView.Balance > cmd.Rate + 704; //TODO
            })
           .WithMessage(a => $"Not enougth coins for create game. Balance should be more then {a.Rate + 704} UBX");
        RuleFor(a => a.Rounds).GreaterThanOrEqualTo(1);
    }
}