using App.Data.Criterias.Core;
using App.Data.Entities.Tasks;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace App.Data.Criterias.Tasks {
    public class TaskByCreatedId : ACriteria<TaskEntity> {
        private readonly Guid[] _createdById;
        private readonly bool _exclude;
        public TaskByCreatedId(Guid[] createdById, bool exclude = false) {
            _createdById = createdById;
            _exclude = exclude;
        }
        public override Expression<Func<TaskEntity, bool>> Build() {
            if (!_exclude)
                return a => _createdById.Contains(a.CreatedById);
            else
                return a => !_createdById.Contains(a.CreatedById);
        }
    }
}
