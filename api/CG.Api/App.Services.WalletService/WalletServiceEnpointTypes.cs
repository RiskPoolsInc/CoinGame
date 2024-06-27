namespace App.Services.WalletService;

public enum WalletServiceEnpointTypes {
    GenerateWallet = 1,
    RefundCoins = 2,
    GenerateTransaction = 3,
    GetBalance = 4,
    TransactionIsCompleted = 5,
}