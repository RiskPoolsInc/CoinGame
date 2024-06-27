using App.Core.Requests.Constants;
using App.Core.ViewModels.Dictionaries;
using App.Security.Annotation;

namespace App.Core.Requests.Dictionaries.Housekeeping {
    [Access]
    public class GetLocalesRequest : CachedRequest<LocaleView[]> {
        public GetLocalesRequest() : base(CacheKeys.Dictionaries.Locales) {
        }
    }
}
