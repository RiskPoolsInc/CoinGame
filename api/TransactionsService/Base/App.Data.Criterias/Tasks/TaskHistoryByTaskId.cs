using App.Data.Criterias.Core;
using App.Data.Entities.Tasks;
using System;
using System.Linq.Expressions;

namespace App.Data.Criterias.Tasks {
    public class TaskHistoryByTaskId : ACriteria<TaskHistory> {
        public TaskHistoryByTaskId(Guid taskId) {
            _taskId = taskId;
        }
        private readonly Guid _taskId;

        public override Expression<Func<TaskHistory, bool>> Build() {
            return a => a.TaskId == _taskId;
        }
    }
}
