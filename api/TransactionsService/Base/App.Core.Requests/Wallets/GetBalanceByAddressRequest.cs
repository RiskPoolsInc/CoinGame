using App.Core.ViewModels.Wallets;

namespace App.Core.Requests.Wallets;

public class GetBalanceByAddressRequest : IRequest<WalletView> {
    public string Address { get; set; }

    public GetBalanceByAddressRequest() {
    }

    public GetBalanceByAddressRequest(string address) {
        Address = address;
    }
}