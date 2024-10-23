using App.Data.Storage.Core;
using App.Interfaces.Data.Storage;
using App.Interfaces.Data.Storage.Core;

namespace App.Data.Storage.Blobs; 

public class ImageStorage : BaseStorage, IImageStorage {
    public ImageStorage(IStorageClient client) : base(client, ContainerNames.Images) {
    }
}