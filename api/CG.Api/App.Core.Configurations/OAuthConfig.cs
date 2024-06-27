using App.Core.Configurations.Annotaions;
using App.Interfaces.Core.Configurations;

namespace App.Core.Configurations; 

[ConfigurationName("OAuth")]
public class OAuthConfig : IConfig {
    private const string APP_KEY = "PBZ";

    public OAuthConfig() {
        Ttl = 20;
        Audience = APP_KEY;
    }

    [ConfigurationName("secret")]
    public string ClientSecret { get; set; }

    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int Ttl { get; set; }
}