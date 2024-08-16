using App.Common.Helpers;
using App.Core.Commands.Transactions;
using App.Core.Enums;
using App.Core.ViewModels.Games;
using App.Core.ViewModels.Transactions;
using App.Data.Criterias.Transactions.TransactionGameDeposit;
using App.Data.Entities.Games;
using App.Data.Entities.Transactions;
using App.Interfaces.Repositories.Games;
using App.Interfaces.Repositories.Transactions;
using App.Services.WalletService;

namespace App.Core.Commands.Handlers.Transactions;

public class GameServiceTransactionHandler : IRequestHandler<GameServiceTransactionComand, TransactionServiceView> {
    private readonly ITransactionServiceRepository _transactionServiceRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IWalletService _walletService;
    private readonly ITransactionGameDepositRepository _transactionGameDepositRepository;

    public GameServiceTransactionHandler(ITransactionServiceRepository     transactionServiceRepository,
                                         IGameRepository                   gameRepository, IWalletService walletService,
                                         ITransactionGameDepositRepository transactionGameDepositRepository) {
        _transactionServiceRepository = transactionServiceRepository;
        _gameRepository = gameRepository;
        _walletService = walletService;
        _transactionGameDepositRepository = transactionGameDepositRepository;
    }

    public async Task<TransactionServiceView> Handle(GameServiceTransactionComand request, CancellationToken cancellationToken) {
        if (_walletService.EqualGameDepositAndServiceWallets ||
            _walletService.ExternalServiceTransactionsEnable ||
            _walletService.ServicePercent == 0)
            return null;

        var game = await _gameRepository.Get(request.GameId).SingleAsync<Game, GameView>(cancellationToken);

        var depositTransaction = await _transactionGameDepositRepository.Where(new TransactionGameDepositByGameId(game.Id))
                                                                        .SingleAsync<TransactionGameDeposit, TransactionGameDepositView>(
                                                                             cancellationToken);

        var gameDepositWallet = _walletService.GameDepositWallet;

        var serviceTransaction = new TransactionService {
            GameId = game.Id,
            WalletHashFrom = depositTransaction.Receivers.Single(a => a.TypeId == (int)TransactionReceiverTypes.GameDepositWallet)
                                               .WalletHash,
            Sum = 0,
            Fee = 0,
            StateId = (int)TransactionStateTypes.Created,
            SkipTransactionReasonId = 1,
        };

        var generatedTransaction = await _walletService.GenerateTransactionService(game.RoundSum);


        _transactionServiceRepository.Add(serviceTransaction);
        await _transactionServiceRepository.SaveAsync(default);

        return await _transactionServiceRepository.Get(serviceTransaction.Id)
                                                  .SingleAsync<TransactionService, TransactionServiceView>(cancellationToken);
    }
}