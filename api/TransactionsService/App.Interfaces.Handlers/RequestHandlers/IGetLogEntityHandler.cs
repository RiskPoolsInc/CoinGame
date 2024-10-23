using App.Core.Requests.LogEntitites;
using App.Core.ViewModels.LogEntities;

using MediatR;

namespace App.Interfaces.Handlers.RequestHandlers;

public interface IGetLogEntityHandler : IRequestHandler<GetLogEntityRequest, LogEntityView> {
}