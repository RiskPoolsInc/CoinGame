using App.Data.Criterias.Core;
using App.Data.Criterias.Core.Helpers;
using App.Data.Entities.Security;
using System;
using System.Linq.Expressions;

namespace App.Data.Criterias.AuditLogs {
    public class AuditLogFilter : PagedCriteria<AuditLog> {
        public int? TypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public override Expression<Func<AuditLog, bool>> Build() {
            var criteria = True;
            if (TypeId.HasValue)
                criteria = criteria.And(new AuditLogByTypeId(TypeId.Value));
            if (StartDate.HasValue)
                criteria = criteria.And(new AuditLogCreatedOnGreaterThan(StartDate.Value));
            if (EndDate.HasValue)
                criteria = criteria.And(new AuditLogCreatedOnLessThan(EndDate.Value));

            if (String.IsNullOrWhiteSpace(Sort))
                SetSortBy(a => a.CreatedOn);

            return criteria.Build();
        }
    }
}
