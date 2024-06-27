using App.Data.Criterias.Core;
using App.Data.Entities.Tasks;
using System;
using System.Linq.Expressions;

namespace App.Data.Criterias.Tasks {
    public class TaskNoteByTaskId : OrderCriteria<TaskNote> {
        private readonly Guid _taskId;
        public TaskNoteByTaskId(Guid taskId) {
            _taskId = taskId;
        }

        public override Expression<Func<TaskNote, bool>> Build() {
            return a => a.TaskId == _taskId;
        }
    }
}
