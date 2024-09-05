using System.Reflection;

using App.Common;

using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace App.Web.Core.ParameterFilters; 

public class CustomSchemaFilter : CustomParameterBase, ISchemaFilter {
    public CustomSchemaFilter(IServiceProvider serviceProvider) : base(serviceProvider) {
    }

    public void Apply(OpenApiSchema schema, SchemaFilterContext context) {
        if (context.MemberInfo == null)
            return;
        var propertyAttribute = (DictionaryTypeAttribute)context.MemberInfo.GetCustomAttribute(typeof(DictionaryTypeAttribute));

        if (propertyAttribute == null)
            return;
        var dictionary = GetDictionary(propertyAttribute.Type);

        if (dictionary.Count == 0)
            return;
        schema.Description = GetDescriptionValues(dictionary);
    }
}