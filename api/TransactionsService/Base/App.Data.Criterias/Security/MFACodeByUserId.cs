using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.Security;

namespace App.Data.Criterias.Security;

public class MFACodeByUserId : ACriteria<MultiFactorAuthCode>
{
    private readonly Guid _userId;

    public MFACodeByUserId(Guid userId)
    {
        _userId = userId;
    }

    public override Expression<Func<MultiFactorAuthCode, bool>> Build()
    {
        return a => a.UserId == _userId;
    }
}