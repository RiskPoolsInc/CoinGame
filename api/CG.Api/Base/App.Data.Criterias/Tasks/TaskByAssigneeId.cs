using App.Data.Criterias.Core;
using App.Data.Entities.Tasks;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace App.Data.Criterias.Tasks
{
    public class TaskByAssigneeId : ACriteria<TaskEntity>
    {
        private readonly Guid?[] _assigneeId;
        private readonly bool _exclude;
        private readonly bool _emptyValuesIncluded;

        public TaskByAssigneeId(Guid?[] assigneeId, bool exclude = false)
        {
            _assigneeId = assigneeId;
            _exclude = exclude;
        }

        public override Expression<Func<TaskEntity, bool>> Build()
        {
            if (!_exclude)
                return a => _assigneeId.Contains(a.AssignedToId) || a.TaskExecutions.Any(s => _assigneeId.Contains(s.UserId));
            else
                return a => !_assigneeId.Contains(a.AssignedToId) && a.TaskExecutions.All(s => !_assigneeId.Contains(s.UserId));
        }
    }
}