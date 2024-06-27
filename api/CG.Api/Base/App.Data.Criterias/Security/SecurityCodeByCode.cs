using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.Security;

namespace App.Data.Criterias.Security;

public class SecurityCodeByCode : ACriteria<SecurityCode>
{
    private readonly string _code;

    public SecurityCodeByCode(string code)
    {
        _code = code;
    }

    public override Expression<Func<SecurityCode, bool>> Build()
    {
        return a => a.Code == _code;
    }
}