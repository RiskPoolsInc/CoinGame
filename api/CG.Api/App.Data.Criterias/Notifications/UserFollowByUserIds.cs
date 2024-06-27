using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.Notifications;

namespace App.Data.Criterias.Notifications;

public class UserFollowByUserIds : ACriteria<UserFollow>
{
    private readonly Guid[] _userIds;

    public UserFollowByUserIds(Guid[] userIds)
    {
        _userIds = userIds;
    }

    public override Expression<Func<UserFollow, bool>> Build()
    {
        if (_userIds.Length == 1)
            return a => a.UserId == _userIds[0];
        return a => _userIds.Contains(a.UserId);
    }
}