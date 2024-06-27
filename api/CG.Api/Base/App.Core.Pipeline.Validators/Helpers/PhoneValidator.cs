using System.Text.RegularExpressions;

using FluentValidation;

namespace App.Core.Pipeline.Validators.Helpers;

public static class PhoneValidatorExtension {
    public static IRuleBuilderOptions<T, string> Phone<T>(this IRuleBuilder<T, string> ruleBuilder) {
        return ruleBuilder
           .Must(value => string.IsNullOrWhiteSpace(value) || Regex.IsMatch(value, @"^[+][0-9]{8,20}$"));
    }
}