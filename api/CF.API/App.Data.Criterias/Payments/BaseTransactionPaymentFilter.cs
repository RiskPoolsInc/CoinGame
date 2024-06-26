using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.Payments;

namespace App.Data.Criterias.Payments;

public class BaseTransactionPaymentFilter : PagedCriteria<Transaction>
{
    public override Expression<Func<Transaction, bool>> Build()
    {
        var criteria = True;

        if (string.IsNullOrWhiteSpace(Sort))
            SetSortBy(a => a.CreatedOn);

        return criteria.Build();
    }

    public override IQueryable<Transaction> OrderBy(IQueryable<Transaction> source)
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