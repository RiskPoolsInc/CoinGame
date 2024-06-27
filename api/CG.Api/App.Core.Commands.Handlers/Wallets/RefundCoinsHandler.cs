using App.Common.Helpers;
using App.Core.Commands.Wallets;
using App.Core.Enums;
using App.Core.ViewModels.Transactions;
using App.Data.Entities.Transactions;
using App.Interfaces.Repositories.Games;
using App.Interfaces.Repositories.Transactions;
using App.Interfaces.Repositories.Wallets;
using App.Services.WalletService;

namespace App.Core.Commands.Handlers.Wallets;

public class RefundCoinsHandler : IRequestHandler<RefundCoinsCommand, TransactionUserRefundView> {
    private readonly ITransactionRefundRepository _refundRepository;
    private readonly IWalletRepository _walletRepository;
    private readonly IGameRepository _gameRepository;
    private readonly WalletService _walletService;

    public RefundCoinsHandler(ITransactionRefundRepository refundRepository,
                              IWalletRepository            walletRepository,
                              IGameRepository              gameRepository,
                              WalletService                walletService) {
        _refundRepository = refundRepository;
        _walletRepository = walletRepository;
        _gameRepository = gameRepository;
        _walletService = walletService;
    }

    public async Task<TransactionUserRefundView> Handle(RefundCoinsCommand request, CancellationToken cancellationToken) {
        var wallet = await _walletRepository.FindAsync(request.WalletId, cancellationToken);

        if (await _gameRepository.AnyAsync(a => a.WalletId == request.WalletId && a.StateId != (int)GameStates.Completed,
                cancellationToken))
            throw new Exception("Any game not completed");
        var generatedTransactionRefund = await _walletService.GenerateTransactionRefund(wallet.Hash, wallet.PrivateKey);

        var transaction = new TransactionUserRefund() {
            WalletHashFrom = generatedTransactionRefund.WalletFrom,
            WalletFromId = wallet.Id,
            TransactionHash = generatedTransactionRefund.Hash,
            Sum = generatedTransactionRefund.Sum,
            Fee = generatedTransactionRefund.Fee,
            StateId = (int)TransactionStates.Created
        };
        _refundRepository.Add(transaction);
        wallet.RefundId = transaction.Id;
        await _gameRepository.SaveAsync(default);

        return await _refundRepository.Get(transaction.Id)
                                      .SingleAsync<TransactionUserRefund, TransactionUserRefundView>(CancellationToken.None);
    }
}