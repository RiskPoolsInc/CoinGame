using App.Web.Core.HAL.Generators;
using App.Web.Core.HAL.Links;
using App.Web.Core.Metadata;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace App.Web.Core.HAL.Builders;

public class HalSpecification : HalBuilder, IHalSpecificationBuilder, IHalSpecification {
    private readonly Catalog _catalog;
    private readonly IPathGenerator _pathGenerator;

    public HalSpecification(string title, Catalog catalog, IPathGenerator pathGenerator, ISchemaGenerator schemaGenerator) :
        base(schemaGenerator) {
        Version = new Uri(HalConstants.Spec);
        Title = title;
        _catalog = catalog;
        _pathGenerator = pathGenerator;
    }

    public string Title { get; }
    public Uri Version { get; }
    public Uri Id { get; private set; }

    public IHalSpecification AddLink<TController>(string methodName, JSchema schema = null) {
        var actions = _catalog.GetControllerInfo<TController>().GetDescriptors(methodName);

        if (actions.Length == 0)
            throw new ArgumentException($"Method {methodName} does not exists on the controller {typeof(TController).FullName}");

        Array.ForEach(actions, a => {
            var builder = new HalLinkAdapter(SchemaGenerator, a.MethodInfo, a)
                         .Make()
                         .AddHref(_pathGenerator.GetAbsolutePath(a));

            if (schema != null) {
                schema.SchemaVersion = Version;
                builder.AddProperties(schema);
            }

            LinkBuilders.Add(builder);
        });
        return this;
    }

    public IHalSpecification AddTypedSchema<T>() {
        PropertiesBuilder.AddProperties<T>();
        return this;
    }

    public IHalSpecification AddSchema(JSchema schema) {
        if (schema == null)
            throw new ArgumentNullException(nameof(schema));

        if (schema.Id != null)
            Id = schema.Id;
        PropertiesBuilder.AddProperties(schema);
        return this;
    }

    /// <summary>
    ///     Build HAL+JSON
    /// </summary>
    /// <returns>HAL+JSON</returns>
    public override JObject Build() {
        var hal = new JObject();

        if (Id != null)
            hal.Add(HalConstants.Id, Id);
        hal.Add(HalConstants.Version, Version);
        hal.Add(HalConstants.Type, "object");

        if (!string.IsNullOrWhiteSpace(Title))
            hal.Add(HalConstants.Title, Title);

        if (PropertiesBuilder.HasProperties)
            hal.Add(HalConstants.Properties, PropertiesBuilder.Build());

        if (LinkBuilders.Count > 0)
            hal.Add(HalConstants.Links, CollectLinks());

        return hal;
    }

    private JArray CollectLinks() {
        var links = new JArray();

        foreach (var halLinkBuilder in LinkBuilders)
            links.Add(halLinkBuilder.Build());
        return links;
    }
}