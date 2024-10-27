namespace App.Core.ViewModels.External;

public class SystemTransactionResultView
{
    public TransactionGameLoseView[] GameLoseTransactions { get; set; }
    public TransactionGameRewardView[] GameRewardsTransactions { get; set; }
}