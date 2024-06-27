using App.Data.Criterias.Core;
using App.Data.Entities.Notifications;
using System;
using System.Linq.Expressions;

namespace App.Data.Criterias.Notifications {
    public abstract class FollowByFollowerId<TEntity> : ACriteria<TEntity> where TEntity: Follow {
        public FollowByFollowerId(Guid followerId) {
            _followerId = followerId;
        }
        private readonly Guid _followerId;

        public override Expression<Func<TEntity, bool>> Build() {
            return a => a.FollowerId == _followerId;
        }
    }
}
