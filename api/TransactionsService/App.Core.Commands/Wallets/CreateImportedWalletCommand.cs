using App.Core.Enums;

namespace App.Core.Commands.Wallets;

public class CreateImportedWalletCommand : CreateWalletCommand {
    public override int TypeId { get; set; } = (int)WalletTypes.Imported;
}