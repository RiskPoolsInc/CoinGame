using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.Security;

namespace App.Data.Criterias.Security;

public class PasswordHistoryByUserId : ACriteria<PasswordHistory>
{
    private readonly Guid _userId;

    public PasswordHistoryByUserId(Guid userId)
    {
        _userId = userId;
    }

    public override Expression<Func<PasswordHistory, bool>> Build()
    {
        return a => a.UserId == _userId;
    }
}