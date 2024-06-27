using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace App.Web.Core.ModelBinders;

public class StringModelBinder : IModelBinder {
    private readonly IModelBinder _fallbackBinder;

    public StringModelBinder(IModelBinder fallbackBinder) {
        _fallbackBinder = fallbackBinder ?? throw new ArgumentNullException(nameof(fallbackBinder));
    }

    public Task BindModelAsync(ModelBindingContext bindingContext) {
        if (bindingContext == null)
            throw new ArgumentNullException(nameof(bindingContext));

        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueProviderResult == ValueProviderResult.None)
            return _fallbackBinder.BindModelAsync(bindingContext);

        var valueAsString = valueProviderResult.FirstValue;

        if (string.IsNullOrEmpty(valueAsString))
            return _fallbackBinder.BindModelAsync(bindingContext);

        var result = valueAsString.Trim();
        bindingContext.Result = ModelBindingResult.Success(result);

        return Task.CompletedTask;
    }
}