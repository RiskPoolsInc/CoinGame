using App.Core.ViewModels.Transactions;

namespace App.Core.Commands.Payments;

public class CheckExistTransactionInBlockChainCommand : IRequest<TransactionView>
{
    public CheckExistTransactionInBlockChainCommand(Guid id)
    {
        TransactionId = id;
    }

    public CheckExistTransactionInBlockChainCommand()
    {
    }

    public Guid TransactionId { get; set; }
}