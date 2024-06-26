using App.Core.Requests.Constants;
using App.Core.ViewModels.Dictionaries;
using App.Security.Annotation;

namespace App.Core.Requests.Dictionaries {
    [Access]
    public class GetMFATypesRequest : CachedRequest<MultiFactorAuthTypeView[]> {
        public GetMFATypesRequest() : base(CacheKeys.Dictionaries.MFATypes) {
        }
    }
}
