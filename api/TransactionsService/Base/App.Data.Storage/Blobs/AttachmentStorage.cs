using App.Data.Storage.Core;
using App.Interfaces.Data.Storage;
using App.Interfaces.Data.Storage.Core;

namespace App.Data.Storage.Blobs; 

public class AttachmentStorage : BaseStorage, IAttachmentStorage {
    public AttachmentStorage(IStorageClient client) : base(client, ContainerNames.Attachments) {
    }
}