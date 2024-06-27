using App.Data.Entities.Notifications;
using System;

namespace App.Data.Criterias.Notifications {
    public class TaskFollowByFollowerId : FollowByFollowerId<TaskFollow> {
        public TaskFollowByFollowerId(Guid followerId) : base(followerId) {
        }
    }
}
