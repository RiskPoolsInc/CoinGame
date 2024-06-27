using App.Interfaces.Data.Entities;

namespace App.Interfaces.Repositories.Core;

public interface IBaseRepository<TEntity, TKey> where TEntity : class, IEntity<TKey> {
}