namespace App.Web.Core.HAL;

internal static class HalConstants {
    internal const string Spec = "https://json-schema.org/draft/2019-09/hyper-schema";
    internal const string Name = "name";
    internal const string Id = "$id";
    internal const string Version = "$schema";
    internal const string Type = "type";
    internal const string Href = "href";
    internal const string Templated = "templated";
    internal const string Title = "title";
    internal const string Properties = "properties";
    internal const string Required = "required";
    internal const string Links = "links"; //
    internal const string Body = "Body";
    internal const string MethodOptions = "OPTIONS";
    internal const string Rel = "rel";
    internal const string MediaType = "mediaType";
    internal const string Method = "method";
    internal const string Schema = "schema";
    internal const string Self = "self";
    internal const string ResourceLinks = "_links";
    internal const string Curies = "curies";
    internal const string Embedded = "_embedded";
    internal const string Item = "item";
    internal const string Summary = "summary";

    public const string CURRENT_PAGE = "self";
    public const string PREVIOUS_PAGE = "prev";
    public const string NEXT_PAGE = "next";
    public const string FIRST_PAGE = "first";
    public const string LAST_PAGE = "last";
}