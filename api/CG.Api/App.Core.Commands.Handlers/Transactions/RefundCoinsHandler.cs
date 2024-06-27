using App.Core.Commands.Wallets;
using App.Core.ViewModels.Transactions;

namespace App.Core.Commands.Handlers.Transactions;

public class RefundCoinsHandler : IRequestHandler<RefundCoinsCommand, TransactionView> {
    public Task<TransactionView> Handle(RefundCoinsCommand request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}