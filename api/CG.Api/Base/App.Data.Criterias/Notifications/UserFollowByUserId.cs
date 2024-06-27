using App.Data.Criterias.Core;
using App.Data.Entities.Notifications;
using System;
using System.Linq.Expressions;

namespace App.Data.Criterias.Notifications {
    public class UserFollowByUserId : ACriteria<UserFollow> {
        public UserFollowByUserId(Guid userId) {
            _userId = userId;
        }
        private readonly Guid _userId;

        public override Expression<Func<UserFollow, bool>> Build() {
            return a => a.UserId == _userId;
        }
    }
}
