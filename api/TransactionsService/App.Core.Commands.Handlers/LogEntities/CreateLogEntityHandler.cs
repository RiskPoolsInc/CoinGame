using App.Core.Commands.LogEntities;
using App.Core.Enums;
using App.Core.Requests.LogEntitites;
using App.Core.ViewModels.LogEntities;
using App.Data.Entities.TransactionLogs;
using App.Interfaces.Handlers.RequestHandlers;
using App.Interfaces.Repositories.LogEntities;
using App.Interfaces.Security;

namespace App.Core.Commands.Handlers.LogEntities;

public class CreateLogEntityHandler : IRequestHandler<WalletCreatedLogCommand, LogEntityView>,
                                      IRequestHandler<RequestGenerateWalletLogCommand, LogEntityView>,
                                      IRequestHandler<RequestImportWalletLogCommand, LogEntityView> {
    private readonly ILogEntityRepository _logRepository;
    private readonly IGetLogEntityHandler _getLogEntityHandler;
    private readonly ICurrentRequestClient _context;

    public CreateLogEntityHandler(ILogEntityRepository logRepository, IContextProvider contextProvider,
                                  IGetLogEntityHandler getLogEntityHandler) {
        _logRepository = logRepository;
        _getLogEntityHandler = getLogEntityHandler;
        _context = contextProvider.Context;
    }

    public Task<LogEntityView> Handle(WalletCreatedLogCommand request, CancellationToken cancellationToken) {
        var entity = new LogEntity {
            WalletId = request.WalletId,
            SenderId = _context.ProfileId,
            TypeId = (int)LogEntityTypes.WalletCreated,
        };
        _logRepository.Add(entity);
        _logRepository.SaveAsync(cancellationToken);
        return _getLogEntityHandler.Handle(new GetLogEntityRequest(entity.Id), cancellationToken);
    }

    public Task<LogEntityView> Handle(RequestGenerateWalletLogCommand request, CancellationToken cancellationToken) {
        var entity = new LogEntity {
            SenderId = _context.ProfileId,
            TypeId = (int)LogEntityTypes.RequestGenerateWallet,
        };
        _logRepository.Add(entity);
        _logRepository.SaveAsync(cancellationToken);
        return _getLogEntityHandler.Handle(new GetLogEntityRequest(entity.Id), cancellationToken);
    }

    public Task<LogEntityView> Handle(RequestImportWalletLogCommand request, CancellationToken cancellationToken) {
        var entity = new LogEntity {
            SenderId = _context.ProfileId,
            TypeId = (int)LogEntityTypes.RequestImportWallet,
        };
        _logRepository.Add(entity);
        _logRepository.SaveAsync(cancellationToken);
        return _getLogEntityHandler.Handle(new GetLogEntityRequest(entity.Id), cancellationToken);
    }
}