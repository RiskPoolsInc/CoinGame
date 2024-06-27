using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace App.Web.Core.HAL.Links;

public interface IHalLinkBuilder {
    string LinkName { get; }

    IHalLinkBuilder AddHref(string      href);
    IHalLinkBuilder AddMediaType(string mediaType);
    IHalLinkBuilder AddMethod(string    method);
    IHalLinkBuilder AddName(string      name);
    IHalLinkBuilder AddProperties<T>();
    IHalLinkBuilder AddProperties(Type    type);
    IHalLinkBuilder AddProperties(JSchema schema);
    IHalLinkBuilder AddTitle(string       title);
    IHalLinkBuilder AddRel(string         rel);
    JObject Build();
}