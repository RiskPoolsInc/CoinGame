using App.Web.Core.HAL.Generators;
using App.Web.Core.HAL.Links;
using App.Web.Core.HAL.Properties;

using Newtonsoft.Json.Linq;

namespace App.Web.Core.HAL.Builders;

public abstract class HalBuilder : IHalBuilder {
    public HalBuilder(ISchemaGenerator schemaGenerator) {
        SchemaGenerator = schemaGenerator ?? throw new ArgumentNullException(nameof(schemaGenerator));
        PropertiesBuilder = new HalPropertiesBuilder(schemaGenerator);
        LinkBuilders = new List<IHalLinkBuilder>();
    }

    protected ISchemaGenerator SchemaGenerator { get; }
    protected IHalPropertiesBuilder PropertiesBuilder { get; }
    protected List<IHalLinkBuilder> LinkBuilders { get; }

    public abstract JObject Build();
}