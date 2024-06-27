using App.Core.Commands.Transactions;
using App.Core.ViewModels.External;

namespace App.Core.Commands.Handlers.Transactions;

public class GenerateTransactionHandler : IRequestHandler<GenerateTransactionCommand, GenerateTransactionView> {
    public Task<GenerateTransactionView> Handle(GenerateTransactionCommand request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}