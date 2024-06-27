using App.Interfaces.Data.Storage.Core;

namespace App.Data.Storage.Docker; 

public class DockerStorageBlockProperties : IStorageBlockProperties {
    public string ContentType { get; set; }
}