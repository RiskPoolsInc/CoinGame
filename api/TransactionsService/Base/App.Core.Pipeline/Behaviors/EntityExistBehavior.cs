using App.Core.Exceptions;
using App.Interfaces.Core;

using Autofac;

using FluentValidation;
using FluentValidation.Results;

using MediatR.Pipeline;

namespace App.Core.Pipeline.Behaviors;

public class EntityExistBehavior<TRequest> : IRequestPreProcessor<TRequest> {
    private readonly IComponentContext _container;

    public EntityExistBehavior(IComponentContext container) {
        _container = container;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken) {
        var context = new ValidationContext<TRequest>(request);
        var errors = new List<ValidationFailure>();

        var validators =
            _container.Resolve(typeof(IEnumerable<IEntityExistValidator<TRequest>>)) as IEnumerable<IEntityExistValidator<TRequest>>;

        if (validators.Count() == 0)
            return;

        foreach (var validator in validators) {
            var result = await validator.ValidateAsync(request, cancellationToken);

            if (!result.IsValid)
                errors.AddRange(result.Errors);
        }

        if (errors.Count > 0)
            throw new EntityNotFoundException(errors.Select(a => a.ErrorMessage));
    }
}