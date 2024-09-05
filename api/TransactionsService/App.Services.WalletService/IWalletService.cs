using App.Core.ViewModels.External;

namespace App.Services.WalletService;

public interface IWalletService {
    Task<GeneratedWalletView> GenerateWallet();
    Task<BalanceView> GetBalance(string                                 address);
    Task<TransactionFeeView> TransactionFee(string           from, string            privateKey, decimal sum);
    Task<object> TransactionMaxRate(string           from, string            privateKey, decimal sum);
    Task<TransactionIsCompletedView> CheckTransactionIsCompleted(string hash, CancellationToken cancellationToken = default);
    Task<GenerateTransactionView> GenerateTransactionService(decimal    roundSum);
    Task<GenerateTransactionView> GenerateTransactionGameDeposit(string from,     string  privateKey, decimal sum);
    Task<GenerateTransactionView> GenerateTransactionRefund(string      from,     string  privateKey);
    Task<GenerateTransactionView> GenerateTransactionReward(string      toWallet, decimal sum);
    bool NeedServiceTransaction();
    string ProfitWalletAddress { get; }
}