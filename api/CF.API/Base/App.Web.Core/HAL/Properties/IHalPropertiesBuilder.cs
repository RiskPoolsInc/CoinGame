using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace App.Web.Core.HAL.Properties;

public interface IHalPropertiesBuilder {
    bool HasProperties { get; }
    IHalPropertiesBuilder AddProperties<T>();
    IHalPropertiesBuilder AddProperties(JSchema schema);
    JObject Build();
}