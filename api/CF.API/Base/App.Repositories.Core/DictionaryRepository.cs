using App.Data.Sql.Core.Interfaces;
using App.Interfaces.Data.Entities;
using App.Interfaces.Repositories.Core;

namespace App.Repositories.Core;

public abstract class DictionaryRepository<TEntity> : BaseRepository<TEntity, int>, IDictionaryRepository<TEntity>
    where TEntity : class, IDictionaryEntity<int> {
    public DictionaryRepository(IDbContext context) : base(context) {
    }
}