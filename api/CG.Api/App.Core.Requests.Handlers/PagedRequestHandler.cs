using App.Core.ViewModels;
using App.Interfaces.Core.Requests;
using MediatR;
using IPagedRequest = App.Core.Requests.Interfaces.IPagedRequest;

namespace App.Core.Requests.Handlers {

public abstract class PagedRequestHandler<TRequest, TResult> : IRequestHandler<TRequest, IPagedList<TResult>>
    where TRequest : IRequest<IPagedList<TResult>>, IPagedRequest
{
    public abstract Task<IPagedList<TResult>> Handle(TRequest request, CancellationToken cancellationToken);
}
}