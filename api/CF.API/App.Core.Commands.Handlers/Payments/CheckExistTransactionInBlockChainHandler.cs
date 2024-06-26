using App.Common.Helpers;
using App.Core.Commands.Payments;
using App.Core.ViewModels.Transactions;
using App.Interfaces.Repositories.Payments.ReferralPayments;
using App.Interfaces.Repositories.Payments.TaskRequests;
using App.Interfaces.Services;

namespace App.Core.Commands.Handlers.Payments;

public class CheckExistTransactionInBlockChainHandler : IRequestHandler<CheckExistTransactionInBlockChainCommand,
    TransactionView>
{
    private readonly ITransactionReferralPaymentRepository _transactionReferralPaymentRepository;
    private readonly IBlockChainExplorer _blockChainExplorer;

    public CheckExistTransactionInBlockChainHandler(
        ITransactionReferralPaymentRepository transactionReferralPaymentRepository,
        IBlockChainExplorer blockChainExplorer 
    )
    {
        _transactionReferralPaymentRepository = transactionReferralPaymentRepository;
        _blockChainExplorer = blockChainExplorer;
    }

    public async Task<TransactionView> Handle(CheckExistTransactionInBlockChainCommand request,
        CancellationToken cancellationToken)
    {
        var trView = (TransactionView) null;
        if (await _transactionRequestsPaymentRepository.AnyAsync(request.TransactionId, cancellationToken))
        {
            var hash = await _transactionRequestsPaymentRepository.Get(request.TransactionId)
                .Select(s => s.TransactionHash).SingleAsync(cancellationToken);
            var exist = await _blockChainExplorer.CheckExistHash(hash, cancellationToken);
            if (exist)
            {
                _transactionRequestsPaymentRepository.Update(request.TransactionId,
                    a => a.ExistInBlockChain = true);
                await _transactionRequestsPaymentRepository.SaveAsync(cancellationToken);
            }

            trView = await _transactionRequestsPaymentRepository.Get(request.TransactionId)
                .SingleAsync<TransactionTaskRequestPayment, TransactionView>(cancellationToken);
        }
        else if (await _transactionReferralPaymentRepository.AnyAsync(request.TransactionId, cancellationToken))
        {
            var hash = await _transactionReferralPaymentRepository.Get(request.TransactionId)
                .Select(s => s.TransactionHash).SingleAsync(cancellationToken);
            var exist = await _blockChainExplorer.CheckExistHash(hash, cancellationToken);
            if (exist)
            {
                _transactionReferralPaymentRepository.Update(request.TransactionId,
                    a => a.ExistInBlockChain = true);
                await _transactionReferralPaymentRepository.SaveAsync(cancellationToken);
            }

            trView = await _transactionReferralPaymentRepository.Get(request.TransactionId)
                .SingleAsync<TransactionReferralPairPayment, TransactionView>(cancellationToken);
        }
        else throw new ArgumentException("Transaction not found");

        return trView;
    }
}