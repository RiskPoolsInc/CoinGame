using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Logging;

namespace App.Web.Core.ModelBinders;

public class ModelBinderProvider : IModelBinderProvider {
    public IModelBinder GetBinder(ModelBinderProviderContext context) {
        if (context == null)
            throw new ArgumentNullException(nameof(context));

        if (context.Metadata.ModelType == typeof(DateTime) ||
            context.Metadata.ModelType == typeof(DateTime?))
            return new DateTimeModelBinder();

        if (context.BindingInfo.BindingSource == null && context.Metadata.ModelType == typeof(string)) {
            var logFactory = (ILoggerFactory)context.Services.GetService(typeof(ILoggerFactory));
            return new StringModelBinder(new SimpleTypeModelBinder(typeof(string), logFactory));
        }

        return null;
    }
}