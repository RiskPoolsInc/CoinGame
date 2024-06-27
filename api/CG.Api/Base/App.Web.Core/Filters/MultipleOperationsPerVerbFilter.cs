using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace App.Web.Core.Filters;

public class MultipleOperationsPerVerbFilter : IOperationFilter {
    public void Apply(OpenApiOperation operation, OperationFilterContext context) {
        var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

        if (descriptor != null)
            operation.OperationId = string.Join("_", context.ApiDescription.HttpMethod, context.ApiDescription.GroupName,
                                                descriptor.ControllerName,
                                                descriptor.ActionName);
    }
}