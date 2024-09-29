using App.Core.Commands.AutoPaymentServiceCommands;
using App.Interfaces.Handlers;
using App.Services.AutoPayment;

namespace App.Core.Commands.Handlers.AutoPaymentServiceHandlers;

public class AutoPaymentServiceRunHandler : IAutoPaymentServiceRunHandler {
    private readonly AutoPaymentService _service;

    public AutoPaymentServiceRunHandler(AutoPaymentService service) {
        _service = service;
    }

    public async Task<Unit> Handle(AutoPaymentServiceRunCommand request, CancellationToken cancellationToken) {
        await _service.RunAsync(cancellationToken);
        await Task.Delay(TimeSpan.FromSeconds(1));
        return Unit.Value;
    }
}