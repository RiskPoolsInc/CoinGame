using App.Interfaces.Data.Entities;

namespace App.Interfaces.Repositories.Core;

public interface IDictionaryRepository<TEntity> : IRepository<TEntity, int>
    where TEntity : class, IDictionaryEntity<int> {
}