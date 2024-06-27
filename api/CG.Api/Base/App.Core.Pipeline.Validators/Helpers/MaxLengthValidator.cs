using App.Resources;

using FluentValidation;

namespace App.Core.Pipeline.Validators.Helpers;

public class MaxLengthValidator : AbstractValidator<string> {
    public MaxLengthValidator(string nameProperty, int maxLenght) {
        RuleFor(m => m).MaximumLength(maxLenght).WithMessage(s => string.Format(ErrorMessage.MaxLength, nameProperty, maxLenght));
    }
}

public static class MaxLengthValidatorExtension {
    public static IRuleBuilderOptions<T, string> MaxLength<T>(this IRuleBuilder<T, string> ruleBuilder,
                                                              string                       nameEntity,
                                                              int                          maxLength) {
        return ruleBuilder.SetValidator(new MaxLengthValidator(nameEntity, maxLength));
    }
}