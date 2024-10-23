using App.Core.ViewModels.External;
using App.Services.WalletService.Models;

namespace App.Services.WalletService;

public interface IWalletService {
    Task<GeneratedWalletView> GenerateWallet();
    Task<BalanceView> GetBalance(string                                 address);
    Task<GenerateTransactionView> CalculateTransaction(string           from, string            privateKey, decimal sum);
    Task<TransactionIsCompletedView> CheckTransactionIsCompleted(string hash, CancellationToken cancellationToken = default);
    Task<GenerateTransactionView> GenerateTransactionService(decimal    roundSum);
    Task<GenerateTransactionView> GenerateTransactionGameDeposit(string from,     string  privateKey, decimal sum);
    Task<GenerateTransactionView> GenerateTransactionRefund(string      from,     string  privateKey);
    Task<GenerateTransactionView> GenerateTransactionReward(string      toWallet, decimal sum);
    Task<TransactionGameRewardView[]> GenerateTransactionRewards(GameRewardReceiverModel[] receivers);

    bool NeedServiceTransaction();
    string ProfitWalletAddress { get; }
}