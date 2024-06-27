using App.Data.Entities.Notifications;

namespace App.Data.Criterias.Notifications;

public class UserFollowByFollowerId : FollowByFollowerId<UserFollow>
{
    public UserFollowByFollowerId(Guid followerId) : base(followerId)
    {
    }
}