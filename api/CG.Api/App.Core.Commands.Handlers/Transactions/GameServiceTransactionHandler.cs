using App.Common.Helpers;
using App.Core.Commands.Transactions;
using App.Core.Enums;
using App.Core.ViewModels.Games;
using App.Core.ViewModels.Transactions;
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

    public GameServiceTransactionHandler(ITransactionServiceRepository transactionServiceRepository,
                                         IGameRepository               gameRepository, IWalletService walletService) {
        _transactionServiceRepository = transactionServiceRepository;
        _gameRepository = gameRepository;
        _walletService = walletService;
    }

    public async Task<TransactionServiceView> Handle(GameServiceTransactionComand request, CancellationToken cancellationToken) {
        if (!_walletService.NeedServiceTransaction())
            return null;

        var game = await _gameRepository.Get(request.GameId).SingleAsync<Game, GameView>(cancellationToken);
        var generatedTransaction = await _walletService.GenerateTransactionService(game.RoundSum);

        var serviceTransaction = new TransactionService {
            GameId = game.Id,
            WalletHashFrom = generatedTransaction.WalletFrom,
            TransactionHash = generatedTransaction.Hash,
            Sum = generatedTransaction.Sum,
            Fee = generatedTransaction.Fee,
            StateId = (int)TransactionStateTypes.Created,
            ExistInBlockChain = false,
        };
        _transactionServiceRepository.Add(serviceTransaction);
        await _transactionServiceRepository.SaveAsync(default);

        return await _transactionServiceRepository.Get(serviceTransaction.Id)
                                                  .SingleAsync<TransactionService, TransactionServiceView>(cancellationToken);
    }
}