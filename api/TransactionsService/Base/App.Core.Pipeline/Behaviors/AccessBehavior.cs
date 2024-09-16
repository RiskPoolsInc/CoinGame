using App.Core.Exceptions;
using App.Interfaces.Security;

using MediatR.Pipeline;

namespace App.Core.Pipeline.Behaviors;

public class AccessBehavior<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull {
    private readonly ICurrentRequestClient _currentUser;

    public AccessBehavior(IContextProvider contextProvider) {
        _currentUser = contextProvider.Context;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken) {
        if (!_currentUser.IsAnonymous && _currentUser.ProfileId.HasValue)
            return Task.CompletedTask;

        throw new AccessDeniedException();
    }
}