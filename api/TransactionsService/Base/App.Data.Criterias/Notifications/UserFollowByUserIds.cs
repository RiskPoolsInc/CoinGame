using App.Data.Criterias.Core;
using App.Data.Entities.Notifications;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace App.Data.Criterias.Notifications {
    public class UserFollowByUserIds : ACriteria<UserFollow> {
        public UserFollowByUserIds(Guid[] userIds) {
            _userIds = userIds;
        }

        private readonly Guid[] _userIds;

        public override Expression<Func<UserFollow, bool>> Build() {
            if (_userIds.Length == 1)
                return a => a.UserId == _userIds[0];
            return a => _userIds.Contains(a.UserId);
        }
    }
}