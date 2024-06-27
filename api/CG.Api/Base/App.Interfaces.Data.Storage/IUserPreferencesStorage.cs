using App.Interfaces.Data.Storage.Core;

namespace App.Interfaces.Data.Storage; 

public interface IUserPreferencesStorage : IStorage {
    IStorageFile GetPreferenceFile(Guid userId, string preferenceType, string preferenceName);
}