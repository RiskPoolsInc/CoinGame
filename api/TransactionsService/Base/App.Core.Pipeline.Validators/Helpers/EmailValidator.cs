using App.Resources;

using FluentValidation;
using FluentValidation.Validators;

namespace App.Core.Pipeline.Validators.Helpers;

public class EmailValidator : AbstractValidator<string> {
    public EmailValidator(string nameProperty, int maxLenght) {
        RuleFor(m => m)
           .EmailAddress(EmailValidationMode.Net4xRegex)
           .WithMessage(x => string.Format(ErrorMessage.InvalidFormat, nameProperty));

        RuleFor(m => m).MaxLength(nameProperty, maxLenght);
    }
}

public static class EmailValidatorExtensions {
    public static IRuleBuilderOptions<T, string> EmailValidator<T>(this IRuleBuilder<T, string> ruleBuilder,
                                                                   string                       nameProperty,
                                                                   int                          maxLenght) {
        return ruleBuilder.SetValidator(new EmailValidator(nameProperty, maxLenght));
    }
}