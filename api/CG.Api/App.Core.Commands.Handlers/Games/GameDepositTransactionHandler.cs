using App.Common.Helpers;
using App.Core.Commands.Transactions;
using App.Core.Enums;
using App.Core.ViewModels.Transactions;
using App.Data.Entities.Transactions;
using App.Interfaces.Repositories.Transactions;
using App.Interfaces.Repositories.Wallets;
using App.Services.WalletService;

using Microsoft.EntityFrameworkCore;

namespace App.Core.Commands.Handlers.Games;

public class GameDepositTransactionHandler : IRequestHandler<GameDepositTransactionCommand, TransactionGameDepositView> {
    private readonly WalletService _walletService;
    private readonly IWalletRepository _walletRepository;
    private readonly ITransactionGameDepositRepository _gameDepositRepository;

    public GameDepositTransactionHandler(WalletService                     walletService, IWalletRepository walletRepository,
                                         ITransactionGameDepositRepository gameDepositRepository) {
        _walletService = walletService;
        _walletRepository = walletRepository;
        _gameDepositRepository = gameDepositRepository;
    }

    public async Task<TransactionGameDepositView> Handle(GameDepositTransactionCommand request, CancellationToken cancellationToken) {
        var wallet = await _walletRepository.Get(request.WalletId).SingleAsync(cancellationToken);
        var generatedTransaction = await _walletService.GenerateTransactionGameDeposit(wallet.Hash, wallet.PrivateKey, request.Sum);

        var transaction = new TransactionGameDeposit {
            GameId = request.GameId,
            WalletFromId = request.WalletId,
            WalletHashFrom = generatedTransaction.WalletFrom,
            TransactionHash = generatedTransaction.Hash,
            Sum = generatedTransaction.Sum,
            Fee = generatedTransaction.Fee,
            StateId = (int)TransactionStates.Created,
            ExistInBlockChain = false,
        };
        _gameDepositRepository.Add(transaction);
        await _gameDepositRepository.SaveAsync(cancellationToken);

        return await _gameDepositRepository.Get(transaction.Id)
                                           .SingleAsync<TransactionGameDeposit, TransactionGameDepositView>(cancellationToken);
    }
}