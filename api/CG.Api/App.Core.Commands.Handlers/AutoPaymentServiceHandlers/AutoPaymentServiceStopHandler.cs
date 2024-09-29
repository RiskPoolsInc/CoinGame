using App.Core.Commands.AutoPaymentServiceCommands;
using App.Interfaces.Handlers;
using App.Services.AutoPayment;

namespace App.Core.Commands.Handlers.AutoPaymentServiceHandlers;

public class AutoPaymentServiceStopHandler : IAutoPaymentServiceStopHandler {
    private readonly AutoPaymentService _service;

    public AutoPaymentServiceStopHandler(AutoPaymentService service) {
        _service = service;
    }

    public async Task<Unit> Handle(AutoPaymentServiceStopCommand request, CancellationToken cancellationToken) {
        await Task.Delay(TimeSpan.FromSeconds(1));
        return Unit.Value;
    }
}