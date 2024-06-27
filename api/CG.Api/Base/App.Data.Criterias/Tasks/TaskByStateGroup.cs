using App.Data.Criterias.Core;
using App.Data.Entities.Tasks;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace App.Data.Criterias.Tasks {
    public class TaskByStateGroup : ACriteria<TaskEntity> {
        private readonly int[] _stateGroupId;
        private readonly bool? _exclude;

        public TaskByStateGroup(int[] stateGroupId, bool? exclude = false) {
            _stateGroupId = stateGroupId;
            _exclude = exclude;
        }        

        public override Expression<Func<TaskEntity, bool>> Build() {
            if (!_exclude.HasValue || !_exclude.Value)
                return a => a.State.TaskStateGroups.Any(x => _stateGroupId.Contains(x.TaskStateGroupTypeId));
            else
                return a => !a.State.TaskStateGroups.Any(x => _stateGroupId.Contains(x.TaskStateGroupTypeId));
        }
    }
}
