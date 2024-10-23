using App.Common.Helpers;
using App.Core.Requests.LogEntitites;
using App.Core.ViewModels.LogEntities;
using App.Data.Entities.TransactionLogs;
using App.Interfaces.Handlers.RequestHandlers;
using App.Interfaces.Repositories.LogEntities;

using MediatR;

namespace App.Core.Requests.Handlers.LogEntities;

public class GetLogEntityHandler : IGetLogEntityHandler {
    private readonly ILogEntityRepository _logEntityRepository;

    public GetLogEntityHandler(ILogEntityRepository logEntityRepository) {
        _logEntityRepository = logEntityRepository;
    }

    public Task<LogEntityView> Handle(GetLogEntityRequest request, CancellationToken cancellationToken) {
        return _logEntityRepository.Get(request.Id).SingleAsync<LogEntity, LogEntityView>(cancellationToken);
    }
}