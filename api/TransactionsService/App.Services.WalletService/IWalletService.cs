using App.Core.ViewModels.External;
using App.Services.WalletService.Models;

namespace App.Services.WalletService;

public interface IWalletService {
    public Task<GeneratedWalletView> GenerateWallet(CancellationToken cancellationToken = default);
    public Task<BalanceView> GetBalance(string address, CancellationToken cancellationToken = default);
    public Task<TransactionIsCompletedView> TransactionIsCompleted(string hash, CancellationToken cancellationToken = default);
    public Task<object> TransactionMaxRate(string fromAddress, string privateKey, TransactionReceiverModel[] receivers);
    public Task<TransactionFeeView> TransactionFee(string address, string privateKey, TransactionReceiverModel[] receivers);
    public Task<GenerateTransactionView> GenerateTransaction(string address, string privateKey, TransactionReceiverModel[] toAddresses);
    public string EncryptPrivateKey(string privateKey, Guid key);
    public string DecryptPrivateKey(string encryptedText, Guid key);
}