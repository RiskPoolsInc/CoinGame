using App.Core.ViewModels.External;
using App.Services.WalletService.Models;

namespace App.Services.WalletService;

public interface IWalletService {
    Task<GeneratedWalletView> GenerateWallet();
    Task<BalanceView> GetBalance(Guid importedWalletId);
    Task<TransactionFeeView> TransactionFee(Guid importedWalletId, decimal sum);
    Task<object> TransactionMaxRate(Guid importedWalletId, decimal sum);
    Task<TransactionIsCompletedView> CheckTransactionIsCompleted(string hash, CancellationToken cancellationToken = default);
    Task<GenerateTransactionView> GenerateTransactionService(decimal roundSum);
    Task<GenerateTransactionView> GenerateTransactionGameDeposit(Guid importedWalletId, decimal sum);
    Task<GenerateTransactionView> GenerateTransactionRefund(Guid importedWalletId);
    Task<GenerateTransactionView> GenerateTransactionReward(string toWallet, decimal sum);
    Task<TransactionGameRewardView[]> GenerateTransactionRewards(GameRewardReceiverModel[] receivers);
    bool NeedServiceTransaction();
    string ProfitWalletId { get; }
}