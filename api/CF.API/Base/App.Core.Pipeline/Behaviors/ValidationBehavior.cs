using App.Core.Exceptions;
using App.Interfaces.Security;

using Autofac;

using FluentValidation;
using FluentValidation.Results;

using MediatR.Pipeline;

namespace App.Core.Pipeline.Behaviors;

public class ValidationBehavior<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull {
    private readonly IComponentContext _container;
    private readonly IContextProvider _contextProvider;

    public ValidationBehavior(IComponentContext container, IContextProvider contextProvider) {
        _container = container;
        _contextProvider = contextProvider;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken) {
        var context = new ValidationContext<TRequest>(request);

        if (_contextProvider.Context.IsExecutor && _contextProvider.Context.IsBlocked)
            throw new AccessDeniedException("Account is blocked");

        var errors = new List<ValidationFailure>();

        foreach (var validator in _container.Resolve(typeof(IEnumerable<IValidator<TRequest>>)) as IEnumerable<IValidator<TRequest>>) {
            var result = await validator.ValidateAsync(request, cancellationToken);

            if (!result.IsValid)
                errors.AddRange(result.Errors);
        }

        if (errors.Count > 0)
            throw new ValidationException(errors);
    }
}