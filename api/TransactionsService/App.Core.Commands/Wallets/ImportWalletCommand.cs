using App.Core.ViewModels.Wallets;

namespace App.Core.Commands.Wallets;

public class ImportWalletCommand : IRequest<WalletView> {
    public string Address { get; set; }
    public string PrivateKey { get; set; }
}