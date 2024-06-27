using App.Core.Requests.Constants;
using App.Core.ViewModels.Dictionaries;
using App.Security.Annotation;

namespace App.Core.Requests.Dictionaries {
    [Access]
    public class GetObjectTypesRequest : CachedRequest<ObjectTypeView[]> {
        public GetObjectTypesRequest() : base(CacheKeys.Dictionaries.ObjectTypes) {
        }
    }
}
