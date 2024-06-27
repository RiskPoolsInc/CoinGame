using Newtonsoft.Json.Schema;

namespace App.Web.Core.HAL.Generators;

public interface ISchemaGenerator {
    JSchema Create<T>();
    JSchema Create(Type type);
}