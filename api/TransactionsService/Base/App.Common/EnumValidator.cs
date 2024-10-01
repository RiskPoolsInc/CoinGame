using FluentValidation;

namespace App.Common;

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