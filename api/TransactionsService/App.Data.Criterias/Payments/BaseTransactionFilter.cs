using System.Linq.Expressions;

using App.Data.Criterias.Core;
using App.Data.Entities.Transactions;

namespace App.Data.Criterias.Payments;

public class BaseTransactionFilter : PagedCriteria<ATransaction>
{
    public override Expression<Func<ATransaction, bool>> Build()
    {
        var criteria = True;

        if (string.IsNullOrWhiteSpace(Sort))
            SetSortBy(a => a.CreatedOn);

        return criteria.Build();
    }

    public override IQueryable<ATransaction> OrderBy(IQueryable<ATransaction> source)
    {
        if (string.IsNullOrWhiteSpace(Sort))
            SetSortBy(a => a.CreatedOn);

        return Sort.ToLower() switch
        {
            "createdon" => OrderByDirection(source, a => a.CreatedOn),
            _ => base.OrderBy(source)
        };
    }
}