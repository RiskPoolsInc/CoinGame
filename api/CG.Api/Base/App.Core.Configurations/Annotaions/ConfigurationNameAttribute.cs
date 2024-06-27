namespace App.Core.Configurations.Annotaions;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = false)]
public class ConfigurationNameAttribute : Attribute {
    public ConfigurationNameAttribute(string name) {
        Name = name;
    }

    public string Name { get; }
}