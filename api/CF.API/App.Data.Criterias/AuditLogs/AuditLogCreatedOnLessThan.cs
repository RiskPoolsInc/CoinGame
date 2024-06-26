using System.Linq.Expressions;
using App.Data.Criterias.Core;

namespace App.Data.Criterias.AuditLogs {
    public class AuditLogCreatedOnLessThan : ACriteria<AuditLog> {
        public AuditLogCreatedOnLessThan(DateTime toDate) {
            _toDate = toDate;
        }
        private readonly DateTime _toDate;

        public override Expression<Func<AuditLog, bool>> Build() {
            return a => a.CreatedOn < _toDate;
        }
    }
}
