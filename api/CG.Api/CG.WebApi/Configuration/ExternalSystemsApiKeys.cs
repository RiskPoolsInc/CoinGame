using App.Core.Configurations.Annotaions;

namespace CF.WebApi.Configuration;

[ConfigurationName("ExternalSystemsApiKeys")]
public class ExternalSystemsApiKeys
{
    public ExternalSystemsApiKey[]? ApiKeys { get; set; }
}