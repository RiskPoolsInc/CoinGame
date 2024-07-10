using App.Common.Helpers;
using App.Core.Commands.Transactions;
using App.Core.Enums;
using App.Core.ViewModels.Transactions;
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
    private readonly ITransactionRewardRepository _transactionRewardRepository;

    public CreateTransactionRewardHandler(IWalletService               walletService,
                                          IWalletRepository            walletRepository,
                                          IGameRepository              gameRepository,
                                          ITransactionRewardRepository transactionRewardRepository) {
        _walletService = walletService;
        _walletRepository = walletRepository;
        _gameRepository = gameRepository;
        _transactionRewardRepository = transactionRewardRepository;
    }

    public async Task<TransactionRewardView> Handle(CreateTransactionRewardCommand request, CancellationToken cancellationToken) {
        var gameWallet = await _gameRepository.Get(request.GameId).SingleAsync();
        var wallet = await _walletRepository.Get(gameWallet.WalletId).SingleAsync();

        var generatedTransaction = await _walletService.GenerateTransactionReward(wallet.Hash);

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