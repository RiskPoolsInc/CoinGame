using App.Core.Requests.Handlers.Helpers;
using App.Core.Requests.Transactions;
using App.Core.ViewModels.Transactions;
using App.Data.Criterias.Payments;
using App.Data.Entities.Payments;
using App.Interfaces.Core.Requests;
using App.Interfaces.Repositories;
using App.Interfaces.Security;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Core.Requests.Handlers.Transactions;

public class GetTransactionsHandler : IRequestHandler<GetTransactionsRequest, IPagedList<TransactionView>>
{
    private readonly IRequestHandler<GetBaseBatchPayments, BaseBatchView[]> _getBaseBatchPayments;
    private readonly IMapper _mapper;
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly ICurrentUser _user;

    public GetTransactionsHandler(IUserProfileRepository userProfileRepository,
        IMapper mapper,
        IRequestHandler<GetBaseBatchPayments, BaseBatchView[]> getBaseBatchPayments,
        IContextProvider contextProvider
    )
    {
        _userProfileRepository = userProfileRepository;
        _mapper = mapper;
        _getBaseBatchPayments = getBaseBatchPayments;
        _user = contextProvider.Context;
    }

    public async Task<IPagedList<TransactionView>> Handle(GetTransactionsRequest request,
        CancellationToken cancellationToken
    )
    {
        var filter = _mapper.Map<BaseTransactionPaymentFilter>(request);

        var result = await _userProfileRepository.GetAll()
            .Where(a => a.TransactionPayments.Any())
            .Where(a => _user.IsAdmin || (a.CompanyId == _user.CompanyId))
            .Select(a => a.TransactionPayments)
            .SelectMany(a => a)
            .Include(a => a.TransactionCommissions)
            .ToLookupAsync<Transaction, TransactionView>(filter, cancellationToken);

        if (result.Items.Length > 0)
        {
            var batches = await _getBaseBatchPayments.Handle(new GetBaseBatchPayments
            {
                TransactionIds = result.Items.Select(a => a.Id).ToArray()
            }, cancellationToken);

            foreach (var transactionView in result.Items)
                transactionView.BatchPayments = batches.Where(a => a.TransactionPaymentId.HasValue &&
                                                                   a.TransactionPaymentId.Value == transactionView.Id)
                    .ToArray();
        }

        return result;
    }
}