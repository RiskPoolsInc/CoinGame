using App.Core.Commands.Transactions;
using App.Core.Requests.Wallets;
using App.Core.ViewModels.External;
using App.Interfaces.Handlers.Requests;
using App.Services.WalletService;
using App.Services.WalletService.Models;

namespace App.Core.Commands.Handlers.Transactions;

public class CalculateTransactionFeeHandler : IRequestHandler<CalculateTransactionFeeCommand, TransactionFeeView> {
    private readonly IWalletService _walletService;
    private readonly IMapper _mapper;
    private readonly IGetWalletPrivateKeyHandler _getWalletPrivateKeyHandler;

    public CalculateTransactionFeeHandler(IWalletService              walletService, IMapper mapper,
                                          IGetWalletPrivateKeyHandler getWalletPrivateKeyHandler) {
        _walletService = walletService;
        _mapper = mapper;
        _getWalletPrivateKeyHandler = getWalletPrivateKeyHandler;
    }

    public async Task<TransactionFeeView> Handle(CalculateTransactionFeeCommand request, CancellationToken cancellationToken) {
        var wallet = await _getWalletPrivateKeyHandler.Handle(new GetWalletPrivateKeyRequest(request.WalletId), cancellationToken);
        var receivers = _mapper.Map<TransactionReceiverModel[]>(request.Receivers);
        var calculatedFee = await _walletService.TransactionFee(wallet.Address, wallet.PrivateKey, receivers);
        return calculatedFee;
    }
}