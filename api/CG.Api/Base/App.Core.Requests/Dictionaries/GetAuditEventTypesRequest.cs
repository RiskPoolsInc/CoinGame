using App.Core.Requests.Constants;
using App.Core.ViewModels.Dictionaries;
using App.Security.Annotation;

namespace App.Core.Requests.Dictionaries {
    [Access]
    public class GetAuditEventTypesRequest : CachedRequest<AuditEventTypeView[]> {
        public GetAuditEventTypesRequest() : base(CacheKeys.Dictionaries.AuditEventTypes) {
        }
    }
}
