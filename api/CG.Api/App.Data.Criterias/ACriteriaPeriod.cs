using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Interfaces.Data.Entities;

namespace App.Data.Criterias;

public abstract class ACriteriaPeriod<T> : ACriteria<T> where T : class, IOrderedEntity
{
    protected readonly DateTime? From;
    protected readonly DateTime? To;

    public ACriteriaPeriod(DateTime? from = null, DateTime? to = null)
    {
        From = from;
        To = to;
    }

    public override Expression<Func<T, bool>> Build()
    {
        if (From.HasValue && To.HasValue)
            return a => a.CreatedOn >= From.Value && a.CreatedOn <= To.Value;

        if (From.HasValue)
            return a => a.CreatedOn >= From.Value;

        if (To.HasValue)
            return a => a.CreatedOn <= To.Value;
        return a => true;
    }
}