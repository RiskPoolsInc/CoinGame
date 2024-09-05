using App.Core.Commands.Payments.Abstractions;
using App.Core.ViewModels.Transactions;

namespace App.Core.Commands.Payments;

public class CreateTransactionReferralCommand : ACreateTransactionCommand, IRequest<TransactionView>
{
}