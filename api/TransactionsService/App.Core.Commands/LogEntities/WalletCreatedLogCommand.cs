using App.Core.ViewModels.LogEntities;

namespace App.Core.Commands.LogEntities;

public class WalletCreatedLogCommand : IRequest<LogEntityView> {
    public Guid WalletId { get; set; }
}