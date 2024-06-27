namespace App.Data.Criterias.Core.Interfaces;

public interface IPagedCriteria<TEntity> : ISortCriteria<TEntity>, IPageOptions where TEntity : class
{
}