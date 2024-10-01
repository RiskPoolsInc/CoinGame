namespace App.Interfaces.Data.Storage.Core; 

public interface IStorageDirectory {
    IStorageDirectory GetDirectoryReference(string name);
    IStorageBlock GetBlockBlobReference(string     name);
}