using App.Resources;

using FluentValidation;
using FluentValidation.Internal;

namespace App.Core.Pipeline.Validators.Helpers;

public class GuidNullableValidator : AbstractValidator<Guid?> {
    public GuidNullableValidator(string nameProperty) {
        RuleFor(m => m)
           .Must(a => a.HasValue && a.Value != Guid.Empty)
           .WithErrorMessage(ErrorMessage.IsRequired, nameProperty);
    }
}

public static class GuidNullableValidatorExtensions {
    public static IRuleBuilderOptions<T, Guid?> GuidNullableValidator<T>(this IRuleBuilder<T, Guid?> ruleBuilder,
                                                                         string                      nameProperty = null) {
        if (ruleBuilder is RuleBuilder<T, Guid?> ruleBuilderImpl)
            return ruleBuilderImpl.SetValidator(new GuidNullableValidator(nameProperty ?? ruleBuilderImpl.Rule.PropertyName));

        return ruleBuilder.SetValidator(new GuidNullableValidator(nameProperty));
    }
}