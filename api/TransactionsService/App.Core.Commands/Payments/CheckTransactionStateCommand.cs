using App.Core.ViewModels.Transactions;

namespace App.Core.Commands.Payments;

public class CheckTransactionStateCommand : IRequest<TransactionView>
{
    public CheckTransactionStateCommand(string hash)
    {
        Hash = hash;
    }

    public CheckTransactionStateCommand()
    {
    }

    public string Hash { get; set; }
}