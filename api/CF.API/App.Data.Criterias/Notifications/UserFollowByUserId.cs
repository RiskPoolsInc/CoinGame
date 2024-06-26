using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.Notifications;

namespace App.Data.Criterias.Notifications;

public class UserFollowByUserId : ACriteria<UserFollow>
{
    private readonly Guid _userId;

    public UserFollowByUserId(Guid userId)
    {
        _userId = userId;
    }

    public override Expression<Func<UserFollow, bool>> Build()
    {
        return a => a.UserId == _userId;
    }
}