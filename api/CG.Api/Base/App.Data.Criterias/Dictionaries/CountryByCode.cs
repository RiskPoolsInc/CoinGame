using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.Dictionaries;

namespace App.Data.Criterias.Dictionaries;

public class CountryByCode : ACriteria<Country>
{
    private readonly string _code;

    public CountryByCode(string code)
    {
        _code = code.ToLower();
    }

    public override Expression<Func<Country, bool>> Build()
    {
        return a => a.Code.ToLower() == _code;
    }
}