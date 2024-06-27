using App.Core.Configurations;

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace App.Web.Core.Options;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions> {
    private readonly UrlConfig _config;
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, UrlConfig config) {
        _provider = provider;
        _config = config;
    }

    public void Configure(SwaggerGenOptions options) {
        // add a swagger document for each discovered API version
        // note: you might choose to skip or document deprecated API versions differently
        foreach (var description in _provider.ApiVersionDescriptions)
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
    }

    private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description) {
        var info = new OpenApiInfo {
            Title = string.IsNullOrWhiteSpace(_config.Instance)
                ? $"{AppProperties.AppName} API"
                : $"{AppProperties.AppName} API {_config.Instance}",
            Version = description.ApiVersion.ToString(),
            Description = $"{AppProperties.AppName} is a unique, managed service approach",
            Contact = new OpenApiContact { Name = AppProperties.Company.Name, Url = new Uri(AppProperties.Company.URL) }
        };

        if (description.IsDeprecated)
            info.Description += " This API version has been deprecated.";

        return info;
    }
}