using App.Core.ViewModels.Wallets;

namespace App.Core.Requests.Wallets;

public class GetWalletBalanceRequest : IRequest<WalletBalanceView> {
    public Guid WalletId { get; set; }

    public GetWalletBalanceRequest() {
    }

    public GetWalletBalanceRequest(Guid walletId) {
        WalletId = walletId;
    }
}