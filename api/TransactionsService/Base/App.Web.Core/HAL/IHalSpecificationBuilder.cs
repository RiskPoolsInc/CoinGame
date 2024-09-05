using Newtonsoft.Json.Schema;

namespace App.Web.Core.HAL;

public interface IHalSpecificationBuilder : IHalBuilder {
    IHalSpecification AddTypedSchema<T>();
    IHalSpecification AddSchema(JSchema schema);
}