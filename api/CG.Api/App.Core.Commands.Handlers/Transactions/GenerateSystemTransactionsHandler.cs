using App.Core.Commands.Transactions;
using App.Core.Enums;
using App.Core.ViewModels.External;
using App.Data.Criterias.Games;
using App.Data.Entities.Transactions;
using App.Interfaces.Handlers;
using App.Interfaces.Repositories.Games;
using App.Interfaces.Repositories.Transactions;
using App.Services.WalletService;
using App.Services.WalletService.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Core.Commands.Handlers.Transactions;

public class GenerateSystemTransactionsHandler : IGenerateSystemTransactionsHandler
{
    private readonly IGameRepository _gameRepository;
    private readonly IWalletService _walletService;
    private readonly ITransactionUserRewardRepository _userRewardRepository;
    private readonly ITransactionServiceRepository _serviceRepository;

    public GenerateSystemTransactionsHandler(IGameRepository gameRepository, IWalletService walletService,
        ITransactionUserRewardRepository userRewardRepository, ITransactionServiceRepository serviceRepository)
    {
        _gameRepository = gameRepository;
        _walletService = walletService;
        _userRewardRepository = userRewardRepository;
        _serviceRepository = serviceRepository;
    }

    public async Task<bool> Handle(GenerateSystemTransactionsCommand request, CancellationToken cancellationToken)
    {
        var gamesWinEntities = await _gameRepository.Where(new NotPayedWinGamesFilter())
            .Include(a => a.TransactionUserRewards)
            .Include(a => a.Wallet)
            .ToListAsync(cancellationToken);

        var gamesLoseEntities = await _gameRepository.Where(new NotPayedLoseGamesFilter())
            .Include(a => a.TransactionServices)
            .ToListAsync(cancellationToken);

        if (!gamesWinEntities.Any() && !gamesLoseEntities.Any())
            return true;

        var systemTransactionModel = new SystemTransactionModel();

        var rewardsReceivers = gamesWinEntities.Select(a => new GameRewardReceiverModel
        {
            Address = a.Wallet.Hash,
            Sum = a.RewardSum,
            GameId = a.Id
        }).ToArray();
        systemTransactionModel.GameRewardReceivers = rewardsReceivers;

        var gamesLose = gamesLoseEntities.Select(a => new GameLoseModel
        {
            GameId = a.Id,
            Bet = a.RoundSum
        }).ToArray();

        systemTransactionModel.GamesLose = gamesLose;

        var systemTransactionResult = await _walletService.GenerateSystemTransactions(systemTransactionModel);

        if (systemTransactionResult.GameRewardsTransactions?.Any() ?? false)
            await AddRewards(systemTransactionResult.GameRewardsTransactions);

        if (systemTransactionResult.GameLoseTransactions?.Any() ?? false)
            await AddServiceTransactions(systemTransactionResult.GameLoseTransactions);

        return true;
    }

    private async Task AddServiceTransactions(TransactionGameLoseView[] gamesLoseTransactions)
    {
        var serviceTransactionEntities = gamesLoseTransactions.Select(a => new TransactionService()
            {
                GameId = a.GameId,
                WalletHashFrom = a.WalletFrom,
                TransactionHash = a.Hash,
                Sum = a.Sum,
                StateId = (int)TransactionStateTypes.Created,
                ExistInBlockChain = false
            })
            .ToArray();

        await _serviceRepository.AddRangeAsync(serviceTransactionEntities, default);
        await _serviceRepository.SaveAsync(default);
    }


    private async Task AddRewards(TransactionGameRewardView[] gamesRewardsTransaction)
    {
        var transactionUserRewardEntities = gamesRewardsTransaction.Select(a => new TransactionUserReward
            {
                GameId = a.GameId,
                WalletHashFrom = a.WalletFrom,
                TransactionHash = a.Hash,
                Sum = a.Sum,
                StateId = (int)TransactionStateTypes.Created,
                ExistInBlockChain = false
            })
            .ToArray();

        await _userRewardRepository.AddRangeAsync(transactionUserRewardEntities, default);
        await _userRewardRepository.SaveAsync(default);
    }
}