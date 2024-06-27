using Microsoft.OpenApi.Models;
using Microsoft.Win32.SafeHandles;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace App.Web.Core.Filters;

public class CancellationTokenFilter : IOperationFilter {
    public void Apply(OpenApiOperation operation, OperationFilterContext context) {
        context.ApiDescription.ParameterDescriptions
               .Where(pd =>
                          pd.ModelMetadata?.ContainerType == typeof(CancellationToken) ||
                          pd.ModelMetadata?.ContainerType == typeof(WaitHandle) ||
                          pd.ModelMetadata?.ContainerType == typeof(SafeWaitHandle))
               .ToList()
               .ForEach(pd => {
                    if (operation.Parameters != null) {
                        var cancellationTokenParameter =
                            operation.Parameters.FirstOrDefault(p => p.Name.Equals(pd.Name, StringComparison.OrdinalIgnoreCase));

                        if (cancellationTokenParameter != null)
                            operation.Parameters.Remove(cancellationTokenParameter);
                    }
                });
    }
}