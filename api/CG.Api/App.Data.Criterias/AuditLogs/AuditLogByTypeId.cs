using System.Linq.Expressions;
using App.Data.Criterias.Core;

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
