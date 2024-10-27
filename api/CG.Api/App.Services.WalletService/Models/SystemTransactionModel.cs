namespace App.Services.WalletService.Models;

public class SystemTransactionModel
{
    public GameLoseModel[] GamesLose { get; set; }
    public GameRewardReceiverModel[] GameRewardReceivers { get; set; }
}