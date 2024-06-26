using App.Web.Core.HAL.Generators;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace App.Web.Core.HAL.Links;

public class HalLinkBuilder : IHalLinkBuilder {
    private readonly ISchemaGenerator _generator;
    private string _href;
    private string _mediaType;
    private string _method;
    private string _name;
    private string _rel;
    private JSchema _schema;
    private bool _templated;
    private string _title;

    public HalLinkBuilder(string rel, ISchemaGenerator schemaGenerator) {
        _generator = schemaGenerator;
        LinkName = rel;
    }

    public string LinkName { get; }

    public IHalLinkBuilder AddRel(string rel) {
        _rel = rel;
        return this;
    }

    public IHalLinkBuilder AddName(string name) {
        _name = name;
        return this;
    }

    public IHalLinkBuilder AddTitle(string title) {
        _title = title;
        return this;
    }

    public IHalLinkBuilder AddHref(string href) {
        _href = href;
        return this;
    }

    public IHalLinkBuilder AddMediaType(string mediaType) {
        _mediaType = mediaType;
        return this;
    }

    public IHalLinkBuilder AddMethod(string method) {
        _method = method;
        return this;
    }

    public IHalLinkBuilder AddProperties<T>() {
        _schema = _generator.Create<T>();
        return this;
    }

    public IHalLinkBuilder AddProperties(Type type) {
        _schema = _generator.Create(type);
        return this;
    }

    public IHalLinkBuilder AddProperties(JSchema schema) {
        _schema = schema;
        return this;
    }

    public JObject Build() {
        var link = new JObject();

        if (!string.IsNullOrWhiteSpace(_rel))
            link.Add(HalConstants.Rel, _rel);

        if (!string.IsNullOrWhiteSpace(_name))
            link.Add(HalConstants.Name, _name);

        if (!string.IsNullOrWhiteSpace(_title))
            link.Add(HalConstants.Title, _title);
        link.Add(HalConstants.Href, _href);

        if (_templated)
            link.Add(HalConstants.Templated, _templated);

        if (!string.IsNullOrEmpty(_mediaType))
            link.Add(HalConstants.MediaType, _mediaType);

        if (!string.IsNullOrEmpty(_method))
            link.Add(HalConstants.Method, _method.ToUpper());

        if (_schema != null)
            link.Add(HalConstants.Schema, JObject.Parse(_schema.ToString()));

        return link;
    }

    public IHalLinkBuilder AddTemplated(bool templated = true) {
        _templated = templated;
        return this;
    }
}