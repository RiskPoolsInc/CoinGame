using Microsoft.AspNetCore.Mvc.Controllers;

namespace App.Web.Core.HAL.Generators;

public interface IPathGenerator {
    string GetAbsolutePath(bool                       appendQueryString = false);
    string GetAbsolutePath(ControllerActionDescriptor descriptor);
    string GetAbsolutePath(params string[]            subPaths);
    string GetAbsolutePath(string                     routeName, object values);
}