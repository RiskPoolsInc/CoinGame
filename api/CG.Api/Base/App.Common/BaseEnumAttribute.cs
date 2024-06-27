namespace App.Common;

public class BaseEnumAttribute : Attribute
{
    public object BaseAttribute { get; }

    public BaseEnumAttribute(object baseAttribute)
    {
        BaseAttribute = baseAttribute;
    }
}