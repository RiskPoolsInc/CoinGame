using App.Core.Configurations.Annotaions;
using App.Interfaces.Core.Configurations;

namespace App.Core.Configurations;

[ConfigurationName("Urls")]
public class UrlConfig : IConfig {
    [ConfigurationName("name")]
    public string Instance { get; set; }

    public string ApiUrl { get; set; }
    public string AppUrl { get; set; }
    public string AuthUrl { get; set; }
}