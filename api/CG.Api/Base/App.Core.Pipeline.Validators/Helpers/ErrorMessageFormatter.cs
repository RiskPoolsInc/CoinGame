using System.Linq.Expressions;
using App.Data.Criterias.Core.Helpers;
using FluentValidation;
using FluentValidation.Resources;

namespace App.Core.Pipeline.Validators.Helpers;

public static class ErrorMessageFormatter {
    public static IRuleBuilderOptions<T, TProperty> WithErrorMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule,
                                                                                   string                                 errorMessage,
                                                                                   params string[]                        messageParams) {
        return rule.Configure(config => {
            config.CurrentValidator.Options.ErrorMessageSource =
                new StaticStringSource(string.Format(errorMessage, messageParams));
        });
    }

    public static IRuleBuilderOptions<T, TProperty> WithErrorMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule,
                                                                                   string                                 errorMessage,
                                                                                   params Expression<Func<T, dynamic>>[]
                                                                                       messageParams) {
        return rule.Configure(config => {
            config.CurrentValidator.Options.ErrorMessageSource =
                new StaticStringSource(string.Format(errorMessage, messageParams.Select(a => a.GetProperty())));
        });
    }

    public static IRuleBuilderOptions<T, TProperty> WithErrorMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule,
                                                                                   string                                 errorMessage) {
        return rule.Configure(config => {
            config.CurrentValidator.Options.ErrorMessageSource =
                new StaticStringSource(string.Format(errorMessage, config.PropertyName));
        });
    }
}