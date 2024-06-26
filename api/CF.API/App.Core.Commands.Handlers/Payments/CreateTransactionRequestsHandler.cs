using App.Common.Helpers;
using App.Core.Commands.Payments;
using App.Core.Commands.Payments.TaskRequests;
using App.Core.Commands.TaskTakeRequests;
using App.Core.Enums;
using App.Core.ViewModels.TaskTakeRequests;
using App.Core.ViewModels.Transactions;
using App.Data.Entities.Payments.TaskRequests;
using App.Data.Entities.TaskTakeRequests;
using App.Interfaces.Core;
using App.Interfaces.Repositories;
using App.Interfaces.Repositories.Payments.TaskRequests;
using App.Interfaces.Security;

namespace App.Core.Commands.Handlers.Payments;

public class CreateTransactionRequestsHandler : IRequestHandler<CreateTransactionRequestsCommand, TakeRequestBaseView[]>
{
    private readonly IBatchRequestsPaymentRepository _batchRepository;
    private readonly ITaskTakeRequestRepository _takeRequestRepository;
    private readonly ITransactionRequestsPaymentRepository _transactionRepository;
    private readonly IDispatcher _dispatcher;
    private readonly ITaskRequestPaymentRepository _requestPaymentRepository;
    private readonly ICurrentUser _currentUser;

    public CreateTransactionRequestsHandler(ITaskRequestPaymentRepository requestPaymentRepository,
        IBatchRequestsPaymentRepository batchRepository,
        ITaskTakeRequestRepository takeRequestRepository,
        IContextProvider contextProvider,
        ITransactionRequestsPaymentRepository transactionRepository,
        IDispatcher dispatcher
    )
    {
        _requestPaymentRepository = requestPaymentRepository;
        _batchRepository = batchRepository;
        _takeRequestRepository = takeRequestRepository;
        _transactionRepository = transactionRepository;
        _dispatcher = dispatcher;
        _currentUser = contextProvider.Context;
    }

    public async Task<TakeRequestBaseView[]> Handle(CreateTransactionRequestsCommand request,
        CancellationToken cancellationToken
    )
    {
        var transactionId = await AddTransactionAsync(request, cancellationToken);
        var takeRequestIds = await AddTakeRequestPaymentsAsync(request, transactionId);
        await SetTakeRequestsPayedAsync(takeRequestIds);

        await _takeRequestRepository.SaveAsync(CancellationToken.None);
        
        await AddCommissionAsync(request, transactionId);
        return Array.Empty<TakeRequestBaseView>();
    }

    private async Task AddCommissionAsync(CreateTransactionRequestsCommand request, Guid transactionId)
    {
        if (request.CommissionCommand != null)
        {
            request.CommissionCommand.CoinsCount *= request.BatchPayments.Select(a => a.Items.Length).Sum();
            request.CommissionCommand.TransactionPaymentId = transactionId;
            await _dispatcher.Send(request.CommissionCommand, CancellationToken.None);
        }
    }

    private async Task SetTakeRequestsPayedAsync(Guid[] takeRequestIds)
    {
        await _takeRequestRepository.UpdateWhereAsync(a => takeRequestIds.Contains(a.Id), a =>
                new TaskTakeRequest()
                {
                    Id = a.Id,
                    UpdatedOn = DateTime.UtcNow,
                    StateId = (int) TaskTakeRequestStates.Payed
                }
            , CancellationToken.None);
    }

    private async Task<Guid[]> AddTakeRequestPaymentsAsync(CreateTransactionRequestsCommand request, Guid transactionId)
    {
        var (entitiesCommand, entities) = BatchTaskRequestsPaymentFactory.Get(request.BatchPayments,
            transactionId);
        await _batchRepository.AddRangeAsync(entities, CancellationToken.None);

        var taskRequestPayments = entitiesCommand.Select(batch =>
            {
                var items = batch.command.Items;

                return items.Select(a => new TaskRequestPayment(a.Id, batch.entity.Id)).ToArray();
            }).SelectMany(s => s)
            .ToArray();

        await _requestPaymentRepository.AddRangeAsync(taskRequestPayments, CancellationToken.None);

        var takeRequestIds = taskRequestPayments.Select(s => s.RequestId.Value).Distinct().ToArray();
        return takeRequestIds;
    }

    private async Task<Guid> AddTransactionAsync(CreateTransactionRequestsCommand requests, CancellationToken cancellationToken)
    {
        var transaction = new TransactionTaskRequestPayment
        {
            TransactionHash = requests.TransactionHash,
            Fee = requests.Fee,
            AddressFrom = requests.AddressFrom,
            Sum = requests.Sum,
            CreatedById = _currentUser.UserProfileId,
            CoinType = requests.CoinType!.Value
        };

        await _transactionRepository.AddAsync(transaction, cancellationToken);
        return transaction.Id;
    }
}