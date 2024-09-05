using App.Core.ViewModels.Wallets;

namespace App.Core.Requests.Wallets;

public class GetWalletRequest : IRequest<WalletView> {
    public Guid Id { get; set; }

    public GetWalletRequest()
    {
        
    }

    public GetWalletRequest(Guid id)
    {
        Id = id;
    }
}