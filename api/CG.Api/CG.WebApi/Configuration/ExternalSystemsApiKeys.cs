using App.Core.Configurations.Annotaions;

namespace CG.WebApi.Configuration;

[ConfigurationName("ExternalSystemsApiKeys")]
public class ExternalSystemsApiKeys
{
    public ExternalSystemsApiKey[]? ApiKeys { get; set; }
}