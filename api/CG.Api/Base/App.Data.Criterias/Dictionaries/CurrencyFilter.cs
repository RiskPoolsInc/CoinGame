using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.Dictionaries;

namespace App.Data.Criterias.Dictionaries;

public class CurrencyFilter : OrderCriteria<Currency>
{
    public CurrencyFilter(string name = null, int? direction = null)
    {
        if (string.IsNullOrWhiteSpace(name)) Sort = "Name";

        if (direction == null) Direction = 1;
    }

    public override Expression<Func<Currency, bool>> Build()
    {
        return True.Build();
    }
}