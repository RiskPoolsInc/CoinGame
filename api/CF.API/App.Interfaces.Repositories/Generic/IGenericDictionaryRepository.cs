using App.Interfaces.Data.Entities;

namespace App.Interfaces.Repositories.Generic; 

public interface IGenericDictionaryRepository<TEntity> : IDictionaryRepository<TEntity> where TEntity : class, IDictionaryEntity<int> {
    IDictionary<string, string> GetDictionary();
}