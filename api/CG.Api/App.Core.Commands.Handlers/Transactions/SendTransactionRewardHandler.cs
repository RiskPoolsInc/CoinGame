using App.Common.Helpers;
using App.Core.Commands.Transactions;
using App.Core.Enums;
using App.Core.ViewModels.Games;
using App.Core.ViewModels.Transactions;
using App.Data.Entities.Games;
using App.Data.Entities.Transactions;
using App.Interfaces.Repositories.Games;
using App.Interfaces.Repositories.Transactions;
using App.Interfaces.Repositories.Wallets;
using App.Services.WalletService;

using Microsoft.EntityFrameworkCore;

namespace App.Core.Commands.Handlers.Transactions;

public class SendTransactionRewardHandler : IRequestHandler<SendTransactionRewardCommand, TransactionRewardView> {
    private readonly IWalletService _walletService;
    private readonly IWalletRepository _walletRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IGameRoundRepository _gameRoundRepository;
    private readonly ITransactionUserRewardRepository _transactionUserRewardRepository;

    public SendTransactionRewardHandler(IWalletService               walletService,
                                          IWalletRepository            walletRepository,
                                          IGameRepository              gameRepository,
                                          IGameRoundRepository         gameRoundRepository,
                                          ITransactionUserRewardRepository transactionUserRewardRepository) {
        _walletService = walletService;
        _walletRepository = walletRepository;
        _gameRepository = gameRepository;
        _gameRoundRepository = gameRoundRepository;
        _transactionUserRewardRepository = transactionUserRewardRepository;
    }

    public async Task<TransactionRewardView> Handle(SendTransactionRewardCommand request, CancellationToken cancellationToken) {
        var game = await _gameRepository.Get(request.GameId).SingleAsync<Game, GameView>(default);
        var wallet = await _walletRepository.Get(game.Wallet.Id).SingleAsync();
        var gameReward = game.RewardSum;

        var rewardTransaction = await _walletService.GenerateTransactionReward(wallet.Hash, gameReward);

        var transaction = new TransactionUserReward {
            GameId = request.GameId,
            WalletHashFrom = rewardTransaction.WalletFrom,
            TransactionHash = rewardTransaction.Hash,
            Sum = rewardTransaction.Sum,
            Fee = rewardTransaction.Fee,
            StateId = (int)TransactionStateTypes.Created,
            ExistInBlockChain = false,
        };

        _transactionUserRewardRepository.Add(transaction);
        await _transactionUserRewardRepository.SaveAsync(default);

        return await _transactionUserRewardRepository.Get(transaction.Id).SingleAsync<TransactionUserReward, TransactionRewardView>(default);
    }
}