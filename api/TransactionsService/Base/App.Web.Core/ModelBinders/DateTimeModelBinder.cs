using System.Globalization;

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace App.Web.Core.ModelBinders;

public class DateTimeModelBinder : IModelBinder {
    public Task BindModelAsync(ModelBindingContext bindingContext) {
        if (bindingContext == null)
            throw new ArgumentNullException(nameof(bindingContext));

        var modelName = bindingContext.ModelName;
        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

        if (valueProviderResult == ValueProviderResult.None)
            return Task.CompletedTask;

        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

        var dateStr = valueProviderResult.FirstValue;

        if (!DateTime.TryParse(dateStr, CultureInfo.CurrentCulture, DateTimeStyles.None, out var date)) {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        bindingContext.Result = ModelBindingResult.Success(date.ToUniversalTime());
        return Task.CompletedTask;
    }
}