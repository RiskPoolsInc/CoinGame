using App.Data.Criterias.Core;
using App.Data.Entities.Tasks;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace App.Data.Criterias.Tasks {
    public class TaskByStateId : ACriteria<TaskEntity> {
        private readonly int[] _stateId;
        private readonly bool? _exclude;

        public TaskByStateId(int[] stateId, bool? exclude = false) {
            _stateId = stateId;
            _exclude = exclude;
        }

        public override Expression<Func<TaskEntity, bool>> Build() {
            if (!_exclude.HasValue || !_exclude.Value)
                return a => _stateId.Contains(a.TaskStateId);
            else
                return a => !_stateId.Contains(a.TaskStateId);
        }
    }
}
