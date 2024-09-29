using App.Core.Commands.AutoPaymentServiceCommands;

using MediatR;

namespace App.Interfaces.Handlers;

public interface IAutoPaymentServiceStopHandler : IRequestHandler<AutoPaymentServiceStopCommand, Unit> {
}