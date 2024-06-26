using App.Core.Exceptions;
using App.Interfaces.Security;
using App.Security.Annotation;
using MediatR.Pipeline;

namespace App.Core.Pipeline.Behaviors
{
    public class CustomerAccessBehavior<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly ICurrentUser _currentUser;

        public CustomerAccessBehavior(IContextProvider contextProvider)
        {
            _currentUser = contextProvider.Context;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            if (_currentUser.IsAdmin || _currentUser.IsCustomer)
                return Task.CompletedTask;
            if (typeof(TRequest).IsDefined(typeof(CustomerAccessAttribute), true))
                throw new AccessDeniedException();
            return Task.CompletedTask;
        }
    }
}