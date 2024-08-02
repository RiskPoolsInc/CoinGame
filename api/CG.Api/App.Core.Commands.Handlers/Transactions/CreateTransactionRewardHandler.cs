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

        _transactionRewardRepository.Add(transaction);
        await _transactionRewardRepository.SaveAsync(default);

        return await _transactionRewardRepository.Get(transaction.Id).SingleAsync<TransactionUserReward, TransactionRewardView>(default);
    }
}