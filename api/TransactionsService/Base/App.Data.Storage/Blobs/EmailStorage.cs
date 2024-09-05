using App.Data.Storage.Core;
using App.Interfaces.Data.Storage;
using App.Interfaces.Data.Storage.Core;

namespace App.Data.Storage.Blobs; 

public class EmailStorage : BaseStorage, IEmailStorage {
    public EmailStorage(IStorageClient client) : base(client, ContainerNames.Emails) {
    }
}