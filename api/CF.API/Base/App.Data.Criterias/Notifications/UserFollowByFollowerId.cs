using App.Data.Entities.Notifications;
using System;

namespace App.Data.Criterias.Notifications {
    public class UserFollowByFollowerId : FollowByFollowerId<UserFollow> {
        public UserFollowByFollowerId(Guid followerId) : base(followerId) {
        }
    }
}
