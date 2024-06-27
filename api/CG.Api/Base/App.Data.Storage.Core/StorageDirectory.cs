using App.Interfaces.Data.Storage.Core;

namespace App.Data.Storage.Core; 

public class StorageDirectory : IStorage {
    private readonly IStorageContainer _container;

    private readonly IStorageDirectory _directory;
    private readonly string _directorySeparator;

    public StorageDirectory(IStorageDirectory directory, IStorageContainer container) {
        _directorySeparator = Path.DirectorySeparatorChar.ToString();
        _directory = directory;
        _container = container;
    }

    public IStorage GetDirectory(string name) {
        return new StorageDirectory(_directory.GetDirectoryReference(name), _container);
    }

    public IStorage GetDirectory(DateTime date) {
        return GetDirectory(string.Format("{0:yyyy}{1}{0:MM}{1}{0:dd}", date, _directorySeparator));
    }

    public IStorageFile GetFile(string name) {
        return new StorageFile(_directory.GetBlockBlobReference(name.ToLower()), _container);
    }

    public async Task<string[]> EnumerateFilesAsync(CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }

    public async Task ClearAsync(CancellationToken cancellationToken) {
        await ClearAsync(_directory, cancellationToken);
    }

    private async Task ClearAsync(IStorageDirectory dir, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}