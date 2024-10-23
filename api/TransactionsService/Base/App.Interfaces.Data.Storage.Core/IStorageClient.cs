namespace App.Interfaces.Data.Storage.Core; 

public interface IStorageClient {
    IStorageContainer GetContainerReference(string name);
}