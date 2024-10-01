namespace App.Data.Criterias.Core.Interfaces;

public interface ISortCriteria<T> : ISortOptions
    where T : class
{
    IQueryable<T> OrderBy(IQueryable<T> source);
    IQueryable OrderBy(IQueryable source);
}