using App.Common.Helpers;
using App.Core.Commands.Transactions;
using App.Core.Enums;
using App.Core.ViewModels.Transactions;
using App.Data.Entities.Transactions;
using App.Interfaces.Repositories.Transactions;
using App.Services.WalletService;

using Microsoft.EntityFrameworkCore;

namespace App.Core.Commands.Handlers.Transactions;

public class CheckGameDepositTransactionHandler : IRequestHandler<CheckGameDepositTransactionCommand, TransactionGameDepositView> {
    private readonly WalletService _walletService;
    private readonly ITransactionGameDepositRepository _gameDepositRepository;

    public CheckGameDepositTransactionHandler(WalletService                     walletService,
                                              ITransactionGameDepositRepository gameDepositRepository) {
        _walletService = walletService;
        _gameDepositRepository = gameDepositRepository;
    }

    public async Task<TransactionGameDepositView> Handle(CheckGameDepositTransactionCommand request, CancellationToken cancellationToken) {
        var gameDeposit = await _gameDepositRepository.Where(a => a.GameId == request.GameId)
                                                      .SingleAsync();

        if (gameDeposit.StateId != (int)TransactionStateTypes.Completed) {
            var transactionStatus = await _walletService.CheckTransactionIsCompleted(gameDeposit.TransactionHash);

            if (transactionStatus.IsCompleted) {
                var gameTransactionEntity = await _gameDepositRepository.FindAsync(gameDeposit.Id, cancellationToken);
                gameTransactionEntity.StateId = (int)TransactionStateTypes.Completed;
                await _gameDepositRepository.SaveAsync(cancellationToken);
            }
        }

        return await _gameDepositRepository.Get(gameDeposit.Id)
                                           .SingleAsync<TransactionGameDeposit, TransactionGameDepositView>(cancellationToken);
    }
}