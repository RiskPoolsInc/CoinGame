namespace App.Web.Core.HAL.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class HalAttribute : Attribute {
    public HalAttribute(string rel, string title) {
        Rel = rel;
        Title = title;
    }

    /// <summary>
    ///     rel
    /// </summary>
    public string Rel { get; }

    /// <summary>
    ///     title
    /// </summary>
    public string Title { get; }

    public Type TypeBody { get; set; }
}