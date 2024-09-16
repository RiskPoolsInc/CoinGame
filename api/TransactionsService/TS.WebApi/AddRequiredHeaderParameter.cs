using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace TS.WebApi;

public class AddRequiredHeaderParameter : IOperationFilter {
    public void Apply(OpenApiOperation operation, OperationFilterContext context) {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter {
            Name = "x-api-key",
            Required = true,
            In = ParameterLocation.Header
        });
    }
}