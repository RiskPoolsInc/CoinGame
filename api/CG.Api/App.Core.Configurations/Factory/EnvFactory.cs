namespace App.Core.Configurations.Factory; 

public class EnvFactory : BaseFactory {
    protected override string GetValue(string typeName) {
        return Environment.GetEnvironmentVariable(typeName);
    }
}