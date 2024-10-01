using System.Collections.Concurrent;

using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Serialization;

namespace App.Web.Core.HAL.Generators;

public class ObjectSchemaGenerator : ISchemaGenerator {
    private readonly ConcurrentDictionary<Type, JSchema> _cache = new();
    private readonly JSchemaGenerator _generator = new();

    public ObjectSchemaGenerator() {
        _generator.DefaultRequired = Required.DisallowNull;
        _generator.ContractResolver = new CamelCasePropertyNamesContractResolver();
    }

    public JSchema Create<T>() {
        return Create(typeof(T));
    }

    public JSchema Create(Type type) {
        return _cache.GetOrAdd(type, t => {
            var schema = _generator.Generate(t);
            schema.SchemaVersion = new Uri(HalConstants.Spec);
            return schema;
        });
    }
}