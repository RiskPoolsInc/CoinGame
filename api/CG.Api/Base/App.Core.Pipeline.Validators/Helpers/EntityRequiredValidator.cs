using App.Common.Helpers;
using App.Interfaces.Core;
using App.Interfaces.Data.Entities;
using App.Interfaces.Repositories.Core;
using App.Resources;

using FluentValidation;

namespace App.Core.Pipeline.Validators.Helpers;

public class EntityRequiredValidator<TEntity, TKey> : AbstractValidator<TKey> where TEntity : class, IEntity<TKey> {
    public EntityRequiredValidator(IRepository<TEntity, TKey> repository, string entityName = null) {
        Func<TKey, CancellationToken, Task<bool>> anyAsync = (id, cancellationToken) => repository.AnyAsync(id, cancellationToken);

        var nameEntity = entityName ?? typeof(TEntity).Name.FromCamelStyleToPhrase();

        RuleFor(x => x)
           .MustAsync(anyAsync)
           .WithMessage(id => string.Format(ErrorMessage.EntityNotFound, nameEntity, id));
    }
}

public static class EntityRequiredValidatorExtension {
    public static IRuleBuilderOptions<T, TKey> EntityRequiredValidator<T, TKey, TEntity>(this IRuleBuilder<T, TKey> ruleBuilder,
                                                                                         IRepository<TEntity, TKey> repository,
                                                                                         string                     entityName = null)
        where TEntity : class, IEntity<TKey> {
        return ruleBuilder.SetValidator(new EntityRequiredValidator<TEntity, TKey>(repository, entityName));
    }

    public static IRuleBuilderOptions<T, TKey> EntityRequiredValidator<T, TKey, TEntity>(this IRuleBuilder<T, TKey>     ruleBuilder,
                                                                                         IRepository<TEntity, TKey>     repository,
                                                                                         IEntityViewLocalizationService localization)
        where TEntity : class, IEntity<TKey> {
        return ruleBuilder.SetValidator(new EntityRequiredValidator<TEntity, TKey>(repository, localization?.GetEntityNameView ??
                                                                                   typeof(TEntity).GetType().Name));
    }
}