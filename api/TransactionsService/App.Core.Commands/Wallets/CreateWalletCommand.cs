using App.Core.ViewModels.Wallets;

namespace App.Core.Commands.Wallets;

public class CreateWalletCommand : IRequest<WalletView> {
    public string Address { get; set; }
    public string PrivateKey { get; set; }
    public virtual int TypeId { get; set; }
}