using App.Common.Helpers;
using App.Core.Commands.Transactions;
using App.Core.Enums;
using App.Core.ViewModels.Transactions;
using App.Data.Criterias.Statistics;
using App.Data.Criterias.Transactions;
using App.Data.Entities.Transactions;
using App.Interfaces.Handlers;
using App.Interfaces.Repositories.Games;
using App.Interfaces.Repositories.Transactions;
using App.Services.WalletService;
using App.Services.WalletService.Models;

using Microsoft.EntityFrameworkCore;

namespace App.Core.Commands.Handlers.Transactions;

public class SendRewardsHandler : ISendRewardsHandler {
    private readonly IGameRepository _gameRepository;
    private readonly IWalletService _walletService;
    private readonly ITransactionUserRewardRepository _userRewardRepository;

    public SendRewardsHandler(IGameRepository                  gameRepository, IWalletService walletService,
                              ITransactionUserRewardRepository userRewardRepository) {
        _gameRepository = gameRepository;
        _walletService = walletService;
        _userRewardRepository = userRewardRepository;
    }

    public async Task<TransactionRewardView[]> Handle(SendRewardsCommand request, CancellationToken cancellationToken) {
        var completedWinGamesWithoutTransactions = await _gameRepository.Where(new CompletedWinGamesWithoutTransactionsFilter())
                                                                        .ToListAsync(cancellationToken);

        if (!completedWinGamesWithoutTransactions.Any())
            return new TransactionRewardView[0];

        var rewardsReceivers = completedWinGamesWithoutTransactions.Select(a => new GameRewardReceiverModel {
            Address = a.Wallet.Hash,
            Sum = a.RewardSum,
            GameId = a.Id
        });

        var gamesRewardsTransaction = await _walletService.GenerateTransactionRewards(rewardsReceivers.ToArray());
        var transactionHash = gamesRewardsTransaction.First().Hash;

        var transactionUserRewardEntities = gamesRewardsTransaction.Select(a => new TransactionUserReward {
                                                                        GameId = a.GameId,
                                                                        WalletHashFrom = a.WalletFrom,
                                                                        TransactionHash = a.Hash,
                                                                        Sum = a.Sum,
                                                                        StateId = (int)TransactionStateTypes.Created,
                                                                        ExistInBlockChain = false
                                                                    })
                                                                   .ToArray();

        await _userRewardRepository.AddRangeAsync(transactionUserRewardEntities, default(CancellationToken));
        await _userRewardRepository.SaveAsync(default(CancellationToken));

        return await _userRewardRepository.Where(new TransactionUserRewardByHash(transactionHash))
                                          .ToArrayAsync<TransactionUserReward, TransactionRewardView>(cancellationToken);
    }
}