using System.Data.Common;

using App.Core.Configurations.Annotaions;
using App.Interfaces.Core.Configurations;

using Newtonsoft.Json;

namespace App.Core.Configurations.Factory;

public abstract class BaseFactory : IConfigFactory {
    public T Create<T>() where T : class, IConfig {
        return (T)Create(typeof(T));
    }

    protected abstract string GetValue(string typeName);

    public object Create(Type type) {
        var config = Activator.CreateInstance(type);
        var parameters = ParseConnectionString(type);
        var properties = config.GetType().GetProperties();

        foreach (var property in properties) {
            var attr = property.GetCustomAttributes(typeof(ConfigurationNameAttribute), false)
                               .OfType<ConfigurationNameAttribute>()
                               .FirstOrDefault();
            var keyName = (attr != null ? attr.Name : property.Name).ToLowerInvariant();

            if (parameters.ContainsKey(keyName)) {
                var value = !property.PropertyType.IsArray
                    ? Convert.ChangeType(parameters[keyName], property.PropertyType)
                    : JsonConvert.DeserializeObject((string)parameters[keyName], property.PropertyType);

                property.SetValue(config, value);
            }
        }

        return config;
    }

    private IDictionary<string, object> ParseConnectionString(Type type) {
        var attr = type.GetCustomAttributes(typeof(ConfigurationNameAttribute), false)
                       .OfType<ConfigurationNameAttribute>()
                       .FirstOrDefault();
        var typeName = attr != null ? attr.Name : type.Name;

        var connection = new DbConnectionStringBuilder {
            ConnectionString = GetValue(typeName)
        };
        return (connection.Keys ?? throw new InvalidOperationException()).OfType<string>().ToDictionary(a => a, b => connection[b]);
    }
}