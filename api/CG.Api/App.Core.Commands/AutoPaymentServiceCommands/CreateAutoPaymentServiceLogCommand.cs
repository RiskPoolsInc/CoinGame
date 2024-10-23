using App.Core.Enums;
using App.Core.ViewModels.AutoPaymentServiceLogs;

namespace App.Core.Commands.AutoPaymentServiceCommands;

public class CreateAutoPaymentServiceLogCommand : IRequest<AutoPaymentServiceLogView> {
    public int TypeId { get; set; }

    public CreateAutoPaymentServiceLogCommand() {
    }

    public CreateAutoPaymentServiceLogCommand(int typeId) {
        TypeId = typeId;
    }

    public CreateAutoPaymentServiceLogCommand(AutoPaymentServiceStatuses typeId) {
        TypeId = (int)typeId;
    }
}