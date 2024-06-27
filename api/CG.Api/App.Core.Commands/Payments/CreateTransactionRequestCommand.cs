using App.Core.Commands.Payments.Abstractions;
using App.Core.ViewModels.Transactions;

namespace App.Core.Commands.Payments;

public class CreateTransactionRequestCommand : ACreateTransactionCommand, IRequest<TransactionView>
{
}