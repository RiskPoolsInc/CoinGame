using App.Core.Configurations.Annotaions;

namespace TS.WebApi.Configuration;

[ConfigurationName("ExternalSystemsApiKeys")]
public class ExternalSystemsApiKeys
{
    public ExternalSystemsApiKey[]? ApiKeys { get; set; }
}