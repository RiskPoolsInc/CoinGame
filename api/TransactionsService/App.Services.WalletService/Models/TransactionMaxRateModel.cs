namespace App.Services.WalletService.Models;

public class TransactionMaxRateModel {
    public decimal Fee { get; set; }
    public decimal Sum { get; set; }
    public string WalletFrom { get; set; }
}