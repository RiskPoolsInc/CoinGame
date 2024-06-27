using System.Text;

using App.Web.Core.Helpers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace App.Web.Core.HAL.Generators;

public class PathGenerator : IPathGenerator {
    private readonly HttpContext _httpContext;
    private readonly object _routeData;
    private readonly IUrlHelper _urlHelper;

    public PathGenerator(HttpContext httpContext, IUrlHelper urlHelper) {
        _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
        _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
        _routeData = _urlHelper.ActionContext.RouteData.Values;
    }

    public string GetAbsolutePath(bool appendQueryString = false) {
        return _httpContext.Request.GetAbsolutePath(_urlHelper.Action(string.Empty), appendQueryString);
    }

    public string GetAbsolutePath(ControllerActionDescriptor descriptor) {
        return _httpContext.Request.GetAbsolutePath(_urlHelper.Action(descriptor.ActionName, descriptor.ControllerName, _routeData));
    }

    public string GetAbsolutePath(string routeName, object values) {
        return _httpContext.Request.GetAbsolutePath(_urlHelper.RouteUrl(routeName, values));
    }

    public string GetAbsolutePath(params string[] subPaths) {
        var pathBuilder = new StringBuilder(GetAbsolutePath().TrimEnd('/'));

        foreach (var subPath in subPaths) {
            if (subPath == null)
                continue;
            pathBuilder.Append("/");
            pathBuilder.Append(subPath.Trim('/'));
        }

        return pathBuilder.ToString();
    }
}