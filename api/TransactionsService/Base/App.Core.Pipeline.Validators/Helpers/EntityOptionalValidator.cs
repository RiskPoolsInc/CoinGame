using App.Interfaces.Data.Entities;
using App.Interfaces.Repositories.Core;

using FluentValidation;

namespace App.Core.Pipeline.Validators.Helpers;

public class EntityOptionalValidator<TEntity, TKey> : AbstractValidator<TKey?> where TKey : struct where TEntity : class, IEntity<TKey> {
    public EntityOptionalValidator(IRepository<TEntity, TKey> repository, string entityName = null) {
        RuleFor(id => id.Value).EntityRequiredValidator(repository, entityName).When(id => id.HasValue);
    }
}

public static class ExistEntityValidatorExtension {
    public static IRuleBuilderOptions<T, TKey?> EntityOptionalValidator<T, TKey, TEntity>(this IRuleBuilder<T, TKey?> ruleBuilder,
                                                                                          IRepository<TEntity, TKey>  repository,
                                                                                          string                      entityName = null)
        where TKey : struct
        where TEntity : class, IEntity<TKey> {
        return ruleBuilder.SetValidator(new EntityOptionalValidator<TEntity, TKey>(repository, entityName));
    }
}