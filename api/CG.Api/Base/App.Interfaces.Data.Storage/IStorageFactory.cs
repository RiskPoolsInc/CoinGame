using App.Interfaces.Data.Storage.Core;

namespace App.Interfaces.Data.Storage; 

public interface IStorageFactory {
    T GetStorage<T>() where T : IStorage;
}