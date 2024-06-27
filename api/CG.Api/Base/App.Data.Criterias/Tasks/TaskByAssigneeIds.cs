using App.Data.Criterias.Core;
using App.Data.Entities.Tasks;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace App.Data.Criterias.Tasks {
    public class TaskByAssigneeIds : ACriteria<TaskEntity> {
        public TaskByAssigneeIds(Guid[]? assigneeIds) {
            _assigneeIds = assigneeIds ?? Array.Empty<Guid>();
        }
        
        private readonly Guid[] _assigneeIds;

        public override Expression<Func<TaskEntity, bool>> Build() {
            return a => a.AssignedToId.HasValue && _assigneeIds.Contains(a.AssignedToId.Value);
        }
    }
}
