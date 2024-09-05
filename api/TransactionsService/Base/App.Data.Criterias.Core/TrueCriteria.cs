using System.Linq.Expressions;

using App.Data.Criterias.Core.Interfaces;

namespace App.Data.Criterias.Core;

public class TrueCriteria<TEntity> : ICriteria<TEntity> where TEntity : class {
    public Expression<Func<TEntity, bool>> Build() {
        return a => true;
    }
}