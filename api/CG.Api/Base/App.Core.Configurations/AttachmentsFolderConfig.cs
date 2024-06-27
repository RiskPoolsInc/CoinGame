using App.Core.Configurations.Annotaions;
using App.Interfaces.Core.Configurations;

namespace App.Core.Configurations;

[ConfigurationName("AttachmentsFolder")]
public class AttachmentsFolderConfig : IConfig {
    public string Directory { get; set; }
    public string HostUrl { get; set; }
}