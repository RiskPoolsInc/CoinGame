using System.Reflection;

using App.Common;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace App.Web.Core.ParameterFilters; 

public class CustomParameterFilter : CustomParameterBase, IParameterFilter {
    public CustomParameterFilter(IServiceProvider serviceProvider) : base(serviceProvider) {
    }

    public void Apply(OpenApiParameter parameter, ParameterFilterContext context) {
        if (context.PropertyInfo == null)
            return;
        var propertyAttribute = (DictionaryTypeAttribute)context.PropertyInfo.GetCustomAttribute(typeof(DictionaryTypeAttribute));

        if (propertyAttribute == null)
            return;
        var dictionary = GetDictionary(propertyAttribute.Type);

        if (dictionary.Count == 0)
            return;
        parameter.Description = GetDescriptionValues(dictionary);
        parameter.Schema.Enum = dictionary.Select(p => new OpenApiString(p.Key)).ToList<IOpenApiAny>();
    }
}