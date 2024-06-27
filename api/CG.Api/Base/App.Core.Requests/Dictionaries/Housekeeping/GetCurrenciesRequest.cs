using App.Core.Requests.Constants;
using App.Core.ViewModels.Dictionaries;
using App.Security.Annotation;

namespace App.Core.Requests.Dictionaries.Housekeeping {
    [Access]
    public class GetCurrenciesRequest : CachedRequest<CurrencyView[]> {
        public GetCurrenciesRequest() : base(CacheKeys.Dictionaries.Currencies) {
        }
    }
}
