using App.Data.Storage.Core;
using App.Interfaces.Data.Storage;
using App.Interfaces.Data.Storage.Core;

namespace App.Data.Storage.Blobs; 

public class NamedStorage : BaseStorage, IAttachmentStorage {
    public NamedStorage(IStorageClient client, string containerName) : base(client, containerName) {
    }
}