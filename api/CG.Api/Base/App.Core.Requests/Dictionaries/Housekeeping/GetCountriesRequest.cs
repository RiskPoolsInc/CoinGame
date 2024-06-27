using App.Core.Requests.Constants;
using App.Core.ViewModels.Dictionaries;
using App.Security.Annotation;

namespace App.Core.Requests.Dictionaries.Housekeeping {
    [Access]
    public class GetCountriesRequest : CachedRequest<CountryView[]> {
        public GetCountriesRequest() : base(CacheKeys.Dictionaries.Countries) {
        }
    }
}
