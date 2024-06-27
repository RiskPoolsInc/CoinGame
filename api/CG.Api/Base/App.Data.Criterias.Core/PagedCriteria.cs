using App.Data.Criterias.Core.Interfaces;

namespace App.Data.Criterias.Core;

public abstract class PagedCriteria<TEntity> : OrderCriteria<TEntity>, IPagedCriteria<TEntity>
    where TEntity : class {
    public const int DefaultPageSize = 10;

    public PagedCriteria() {
        Size = DefaultPageSize;
        Page = 1;
    }

    public int? Page { get; set; }
    public int? Size { get; set; }
    public int? Skip { get; set; }
    public bool? GetAll { get; set; }
    public bool? WithDeleted { get; set; }
}