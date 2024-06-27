using App.Data.Criterias.Core;
using App.Data.Entities.Security;
using System;
using System.Linq.Expressions;

namespace App.Data.Criterias.AuditLogs {
    public class AuditLogCreatedOnGreaterThan : ACriteria<AuditLog> {
        public AuditLogCreatedOnGreaterThan(DateTime fromDate) {
            _fromDate = fromDate;
        }
        private readonly DateTime _fromDate;

        public override Expression<Func<AuditLog, bool>> Build() {
            return a => a.CreatedOn >= _fromDate;
        }
    }
}
