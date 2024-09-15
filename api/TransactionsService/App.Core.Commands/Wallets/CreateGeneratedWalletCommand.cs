using App.Core.Enums;

namespace App.Core.Commands.Wallets;

public class CreateGeneratedWalletCommand : CreateWalletCommand {
    public override int TypeId { get; set; } = (int)WalletTypes.Generated;
}