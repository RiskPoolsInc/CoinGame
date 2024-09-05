using Microsoft.Extensions.Configuration;

namespace App.Core.Configurations.Factory; 

public class ConfigFactory : BaseFactory {
    private readonly IConfiguration _configuration;

    public ConfigFactory(IConfiguration configuration) {
        _configuration = configuration;
    }

    protected override string GetValue(string typeName) {
        return _configuration.GetConnectionString(typeName);
    }
}