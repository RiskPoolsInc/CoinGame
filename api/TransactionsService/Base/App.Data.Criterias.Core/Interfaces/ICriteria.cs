using System.Linq.Expressions;

namespace App.Data.Criterias.Core.Interfaces;

public interface ICriteria<TEntity> where TEntity : class
{
    Expression<Func<TEntity, bool>> Build();
}