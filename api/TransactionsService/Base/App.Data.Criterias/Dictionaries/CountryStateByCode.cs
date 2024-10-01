using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.Dictionaries;

namespace App.Data.Criterias.Dictionaries;

public class CountryStateByCode : ACriteria<CountryState>
{
    private readonly string _code;

    public CountryStateByCode(string code)
    {
        _code = code.ToLower();
    }

    public override Expression<Func<CountryState, bool>> Build()
    {
        return a => a.Code.ToLower() == _code;
    }
}