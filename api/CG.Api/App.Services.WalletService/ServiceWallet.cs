namespace App.Services.WalletService;

public class ServiceWallet : OptionProperty {
    public string? PrivateKey { get; set; }
    public double? PercentCoins { get; set; } = 0;
}