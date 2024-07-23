using App.Common.Helpers;
using App.Core.Commands.Transactions;
using App.Core.Enums;
using App.Core.Requests.Handlers.Helpers;
using App.Core.ViewModels.Games;
using App.Core.ViewModels.Transactions;
using App.Data.Entities.GameRounds;
using App.Data.Entities.Games;
using App.Data.Entities.Transactions;
using App.Interfaces.Repositories.Games;
using App.Interfaces.Repositories.Transactions;
using App.Interfaces.Repositories.Wallets;
using App.Services.WalletService;

using Microsoft.EntityFrameworkCore;

namespace App.Core.Commands.Handlers.Transactions;

public class CreateTransactionRewardHandler : IRequestHandler<CreateTransactionRewardCommand, TransactionRewardView> {
    private readonly IWalletService _walletService;
    private readonly IWalletRepository _walletRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IGameRoundRepository _gameRoundRepository;
    private readonly ITransactionRewardRepository _transactionRewardRepository;

    public CreateTransactionRewardHandler(IWalletService               walletService,
                                          IWalletRepository            walletRepository,
                                          IGameRepository              gameRepository,
                                          IGameRoundRepository         gameRoundRepository,
                                          ITransactionRewardRepository transactionRewardRepository) {
        _walletService = walletService;
        _walletRepository = walletRepository;
        _gameRepository = gameRepository;
        _gameRoundRepository = gameRoundRepository;
        _transactionRewardRepository = transactionRewardRepository;
    }

    public async Task<TransactionRewardView> Handle(CreateTransactionRewardCommand request, CancellationToken cancellationToken) {
        var game = await _gameRepository.Get(request.GameId).SingleAsync<Game, GameView>(default);
        var wallet = await _walletRepository.Get(game.Wallet.Id).SingleAsync();

        var rounds = await _gameRoundRepository.Where(a => a.GameId == game.Id)
                                               .OrderBy(a => a.Number)
                                               .ToArrayAsync<GameRound, GameRoundView>(default);
        var gameReward = 0m;

        for (var i = 0; i < rounds.Length; i++) {
            var round = rounds[i];

            if (round.Result.Id == (int)GameRoundResultTypes.Win)
                gameReward += game.RoundSum;
            else
                gameReward -= game.RoundSum;
        }

        var generatedTransaction = await _walletService.GenerateTransactionReward(wallet.Hash, gameReward > 0 ? gameReward : game.RoundSum);

        var transaction = new TransactionUserReward {
            GameId = request.GameId,
            WalletHashFrom = generatedTransaction.WalletFrom,
            TransactionHash = generatedTransaction.Hash,
            Sum = generatedTransaction.Sum,
            Fee = generatedTransaction.Fee,
            StateId = (int)TransactionStateTypes.Created,
            ExistInBlockChain = false,
        };

        _transactionRewardRepository.Add(transaction);
        await _transactionRewardRepository.SaveAsync(default);

        return await _transactionRewardRepository.Get(transaction.Id)
                                                 .SingleAsync<TransactionUserReward, TransactionRewardView>(default);
    }
}