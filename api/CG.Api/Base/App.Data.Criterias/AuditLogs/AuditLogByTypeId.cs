using App.Data.Criterias.Core;
using App.Data.Entities.Security;
using System;
using System.Linq.Expressions;

namespace App.Data.Criterias.AuditLogs {
    public class AuditLogByTypeId : ACriteria<AuditLog> {
        public AuditLogByTypeId(int typeId) {
            _typeId = typeId;
        }
        private readonly int _typeId;

        public override Expression<Func<AuditLog, bool>> Build() {
            return a => a.TypeId == _typeId;
        }
    }
}
