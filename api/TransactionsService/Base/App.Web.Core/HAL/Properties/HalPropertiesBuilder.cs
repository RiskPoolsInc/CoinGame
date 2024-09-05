using App.Web.Core.HAL.Generators;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace App.Web.Core.HAL.Properties;

public class HalPropertiesBuilder : IHalPropertiesBuilder {
    private readonly ISchemaGenerator _schemaGenerator;
    private IDictionary<string, JSchema> _properties;

    public HalPropertiesBuilder(ISchemaGenerator schemaGenerator) {
        _schemaGenerator = schemaGenerator ?? throw new ArgumentNullException(nameof(schemaGenerator));
    }

    public IHalPropertiesBuilder AddProperties<T>() {
        return AddProperties(_schemaGenerator.Create<T>());
    }

    public IHalPropertiesBuilder AddProperties(JSchema schema) {
        if (schema == null)
            throw new ArgumentNullException(nameof(schema));
        _properties = schema.Properties;
        return this;
    }

    public bool HasProperties => _properties != null && _properties.Count > 0;

    public JObject Build() {
        var properties = new JObject();

        if (HasProperties)
            foreach (var property in _properties)
                properties.Add(property.Key, JObject.Parse(property.Value.ToString()));
        return properties;
    }
}