using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Resources;

namespace App.Common.Helpers;

public static class EnumHelper {
    public static T Attribute<T>(this Enum value) {
        var type = value.GetType();
        var memberInfo = type.GetMember(Enum.GetName(type, value)).First();

        if (!memberInfo.IsDefined(typeof(T), true))
            return default;
        return memberInfo.GetCustomAttributes(typeof(T), true).Cast<T>().FirstOrDefault();
    }

    public static string GetDisplayValue(this Enum value) {
        var fieldInfo = value.GetType().GetField(value.ToString());

        var descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

        if (descriptionAttributes == null)
            return string.Empty;

        if (descriptionAttributes.Length > 0 && descriptionAttributes[0].ResourceType != null)
            return LookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);

        return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Name : value.ToString();
    }

    public static IEnumerable<T> GetValues<T>() {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }

    private static string LookupResource(Type resourceManagerProvider, string resourceKey) {
        foreach (var staticProperty in resourceManagerProvider.GetProperties(
                     BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            if (staticProperty.PropertyType == typeof(ResourceManager)) {
                var resourceManager = (ResourceManager)staticProperty.GetValue(null, null);
                return resourceManager.GetString(resourceKey);
            }

        return resourceKey;
    }
}