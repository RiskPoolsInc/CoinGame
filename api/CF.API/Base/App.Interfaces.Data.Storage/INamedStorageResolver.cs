using App.Interfaces.Data.Storage.Core;

namespace App.Interfaces.Data.Storage; 

public interface INamedStorageResolver {
    IStorage GetStorage(string containerName);
}