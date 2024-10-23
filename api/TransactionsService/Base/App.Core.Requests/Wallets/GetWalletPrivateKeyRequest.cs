using App.Core.ViewModels.Wallets;

namespace App.Core.Requests.Wallets;

public class GetWalletPrivateKeyRequest : IRequest<WalletPrivateKeyView> {
    public Guid WalletId { get; set; }

    public GetWalletPrivateKeyRequest() {
    }

    public GetWalletPrivateKeyRequest(Guid walletId) {
        WalletId = walletId;
    }
}