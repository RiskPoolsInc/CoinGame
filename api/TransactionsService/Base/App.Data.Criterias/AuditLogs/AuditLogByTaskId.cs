using App.Data.Criterias.Core;
using App.Data.Entities.Security;
using System;
using System.Linq.Expressions;

namespace App.Data.Criterias.AuditLogs {
    public class AuditLogByTaskId : ACriteria<AuditLog>{
        public AuditLogByTaskId(Guid taskId) {
            _taskId = taskId;
        }
        private readonly Guid _taskId;

        public override Expression<Func<AuditLog, bool>> Build() {
            return a => a.TaskId == _taskId;
        }
    }
}
