using App.Common.Helpers;
using App.Core.Commands.AutoPaymentServiceCommands;
using App.Core.ViewModels.AutoPaymentServiceLogs;
using App.Data.Entities.AutoPaymentServiceLogs;
using App.Interfaces.Repositories.AutoPaymentServiceLogs;

namespace App.Core.Commands.Handlers.AutoPaymentServiceHandlers;

public class CreateAutoPaymentServiceLogHandler : IRequestHandler<CreateAutoPaymentServiceLogCommand, AutoPaymentServiceLogView> {
    private readonly IAutoPaymentServiceLogRepository _autoPaymentServiceLogRepository;

    public CreateAutoPaymentServiceLogHandler(IAutoPaymentServiceLogRepository autoPaymentServiceLogRepository) {
        _autoPaymentServiceLogRepository = autoPaymentServiceLogRepository;
    }

    public async Task<AutoPaymentServiceLogView> Handle(CreateAutoPaymentServiceLogCommand request, CancellationToken cancellationToken) {
        var entity = new AutoPaymentServiceLog() {
            TypeId = request.TypeId
        };
        _autoPaymentServiceLogRepository.Add(entity);
        await _autoPaymentServiceLogRepository.SaveAsync(cancellationToken);
        return await _autoPaymentServiceLogRepository.Get(entity.Id).SingleAsync<AutoPaymentServiceLog, AutoPaymentServiceLogView>(cancellationToken);
    }
}