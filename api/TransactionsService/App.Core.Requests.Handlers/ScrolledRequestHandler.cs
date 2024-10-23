using App.Core.ViewModels;
using App.Interfaces.Core.Requests;
using MediatR;

namespace App.Core.Requests.Handlers {

public abstract class ScrolledRequestHandler<TRequest, TResult> : IRequestHandler<TRequest, ScrolledList<TResult>>
    where TRequest : IRequest<ScrolledList<TResult>>, IScrolledRequest
{
    public abstract Task<ScrolledList<TResult>> Handle(TRequest request, CancellationToken cancellationToken);
}
}