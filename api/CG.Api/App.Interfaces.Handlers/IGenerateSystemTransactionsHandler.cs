using App.Core.Commands.Transactions;
using MediatR;

namespace App.Interfaces.Handlers;

public interface IGenerateSystemTransactionsHandler : IRequestHandler<GenerateSystemTransactionsCommand, bool>
{
}