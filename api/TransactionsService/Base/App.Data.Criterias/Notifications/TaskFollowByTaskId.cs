using App.Data.Criterias.Core;
using App.Data.Entities.Notifications;
using System;
using System.Linq.Expressions;

namespace App.Data.Criterias.Notifications {
    public class TaskFollowByTaskId : ACriteria<TaskFollow> {
        public TaskFollowByTaskId(Guid taskId) {
            _taskId = taskId;
        }
        private readonly Guid _taskId;

        public override Expression<Func<TaskFollow, bool>> Build() {
            return a => a.TaskId == _taskId;
        }
    }
}
