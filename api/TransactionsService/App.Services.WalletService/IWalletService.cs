using App.Core.ViewModels.External;
using App.Services.WalletService.Models;

namespace App.Services.WalletService;

public interface IWalletService {
    Task<GeneratedWalletView> GenerateWallet(CancellationToken cancellationToken = default);
    Task<BalanceView> GetBalance(string                            address);
    Task<TransactionIsCompletedView> TransactionIsCompleted(string hash,        CancellationToken cancellationToken = default);
    Task<object> TransactionMaxRate(string                         fromAddress, string privateKey, TransactionReceiverModel[] receivers);
    Task<TransactionFeeView> TransactionFee(string                 from,        string privateKey, TransactionReceiverModel[] receivers);

    Task<GenerateTransactionView> GenerateTransaction(string                     fromAddress, string privateKey,
                                                      TransactionReceiverModel[] toAddresses);
}