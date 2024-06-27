using App.Core.ViewModels.Wallets;

namespace App.Core.Requests.Wallets;

public class GetWalletBalanceRequest : IRequest<WalletView> {
    public Guid? WalletId { get; set; }
    public string? Address { get; set; }
}