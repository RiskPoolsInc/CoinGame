using FluentValidation;

namespace App.Core.Pipeline.Validators.Helpers;

public class GuidValidator : AbstractValidator<Guid> {
    public GuidValidator(string nameProperty) {
        RuleFor(m => new Guid?(m)).GuidNullableValidator(nameProperty);
    }
}

public static class GuidValidatorExtensions {
    public static IRuleBuilderOptions<T, Guid> GuidValidator<T>(this IRuleBuilder<T, Guid> ruleBuilder,
                                                                string                     nameProperty) {
        return ruleBuilder.SetValidator(new GuidValidator(nameProperty));
    }
}