using System;
using App.Core.ViewModels.Audits;

namespace App.Core.Requests.Audits {
    public class GetAuditLogRequest : PagedRequest<AuditLogView> {
        public GetAuditLogRequest() {
            Size = 20;
        }
        public int? TypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
