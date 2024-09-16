using FluentValidation;

namespace App.Core.Pipeline.Validators.Helpers;

public class EnumValidator<T> : AbstractValidator<int> where T : struct, Enum {
    public EnumValidator() {
        RuleFor(a => a)
           .Must(s => {
                var values = Enum.GetValues<T>();
                var intArray = values.Cast<int>().ToArray();
                return intArray.Contains(s);
            });
    }
}