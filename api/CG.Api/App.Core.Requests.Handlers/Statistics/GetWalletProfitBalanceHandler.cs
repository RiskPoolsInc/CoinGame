using App.Core.ViewModels.External;
using App.Services.WalletService;

using MediatR;

namespace App.Core.Requests.Statistics;

public class GetWalletProfitBalanceHandler : IRequestHandler<GetWalletProfitBalanceRequest, BalanceView> {
    private readonly IWalletService _walletService;

    public GetWalletProfitBalanceHandler(IWalletService walletService) {
        _walletService = walletService;
    }

    public async Task<BalanceView> Handle(GetWalletProfitBalanceRequest request, CancellationToken cancellationToken) {
        var balanceView = await _walletService.GetBalance(_walletService.ProfitWalletAddress);
        return balanceView;
    }
}