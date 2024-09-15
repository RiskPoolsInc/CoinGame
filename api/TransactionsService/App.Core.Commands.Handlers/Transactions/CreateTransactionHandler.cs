using App.Core.Commands.Transactions;
using App.Core.ViewModels.Transactions;

namespace App.Core.Commands.Handlers.Transactions;

public class CreateTransactionHandler: IRequestHandler<CreateTransactionCommand, TransactionView> {
    public Task<TransactionView> Handle(CreateTransactionCommand request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}