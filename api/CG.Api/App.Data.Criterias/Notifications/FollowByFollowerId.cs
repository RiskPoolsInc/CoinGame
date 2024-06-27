using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.Notifications;

namespace App.Data.Criterias.Notifications;

public abstract class FollowByFollowerId<TEntity> : ACriteria<TEntity> where TEntity : Follow
{
    private readonly Guid _followerId;

    public FollowByFollowerId(Guid followerId)
    {
        _followerId = followerId;
    }

    public override Expression<Func<TEntity, bool>> Build()
    {
        return a => a.FollowerId == _followerId;
    }
}