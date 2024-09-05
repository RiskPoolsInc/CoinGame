using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace App.Web.Core.Metadata;

public class Catalog {
    private readonly Lazy<IDictionary<string, ControllerInfo>> _getActions;
    private readonly IActionDescriptorCollectionProvider _provider;

    public Catalog(IActionDescriptorCollectionProvider actionDescriptorProvider) {
        _provider = actionDescriptorProvider;

        _getActions = new Lazy<IDictionary<string, ControllerInfo>>(() => {
            return _provider
                  .ActionDescriptors.Items.OfType<ControllerActionDescriptor>()
                  .GroupBy(a => a.ControllerTypeInfo.FullName)
                  .ToDictionary(
                       a => a.Key,
                       a => new ControllerInfo(
                           a.Key, new ControllerActions(a.ToArray())));
        }, true);
    }

    public ControllerInfo GetControllerInfo<T>() {
        var controllerName = typeof(T).FullName;
        _getActions.Value.TryGetValue(controllerName, out var info);
        return info ?? throw new ArgumentException($"Passed type {controllerName} is not a controller.");
    }
}