using App.Interfaces.Data.Storage;
using App.Interfaces.Data.Storage.Core;

using Autofac;

namespace App.Data.Storage; 

public class StorageFactory : IStorageFactory {
    private readonly IComponentContext _container;

    public StorageFactory(IComponentContext container) {
        _container = container;
    }

    public T GetStorage<T>() where T : IStorage {
        return _container.Resolve<T>();
    }
}