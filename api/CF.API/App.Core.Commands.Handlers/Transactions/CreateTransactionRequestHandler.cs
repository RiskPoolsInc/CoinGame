using App.Common.Helpers;
using App.Core.Commands.Payments;
using App.Core.ViewModels.Transactions;
using App.Data.Entities.Payments.TaskRequests;
using App.Interfaces.Repositories.Payments.TaskRequests;
using App.Interfaces.Security;

namespace App.Core.Commands.Handlers.Transactions;

public class CreateTransactionRequestHandler : IRequestHandler<CreateTransactionRequestCommand, TransactionView>
{
    private readonly ITransactionRequestsPaymentRepository _transactionRepository;
    private readonly ICurrentUser _currentUser;

    public CreateTransactionRequestHandler(ITransactionRequestsPaymentRepository transactionRepository,
        IContextProvider contextProvider
    )
    {
        _transactionRepository = transactionRepository;
        _currentUser = contextProvider.Context;
    }

    public async Task<TransactionView> Handle(CreateTransactionRequestCommand transactionRequest, CancellationToken cancellationToken)
    {
        var entity = new TransactionTaskRequestPayment
        {
            TransactionHash = transactionRequest.TransactionHash,
            Fee = transactionRequest.Fee,
            AddressFrom = transactionRequest.AddressFrom,
            Sum = transactionRequest.Sum,
            CreatedById = _currentUser.UserProfileId,
            CoinType = transactionRequest.CoinType!.Value
        };

        await _transactionRepository.AddAsync(entity, cancellationToken);
        await _transactionRepository.SaveAsync(cancellationToken);
        var modelView = await _transactionRepository.Get(entity.Id)
            .SingleAsync<TransactionTaskRequestPayment, TransactionView>(cancellationToken);
        return modelView;
    }
}