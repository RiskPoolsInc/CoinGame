using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.Security;

namespace App.Data.Criterias.Security;

public class MFACodeByCode : ACriteria<MultiFactorAuthCode>
{
    private readonly string _code;

    public MFACodeByCode(string code)
    {
        _code = code;
    }

    public override Expression<Func<MultiFactorAuthCode, bool>> Build()
    {
        return a => a.Code == _code;
    }
}