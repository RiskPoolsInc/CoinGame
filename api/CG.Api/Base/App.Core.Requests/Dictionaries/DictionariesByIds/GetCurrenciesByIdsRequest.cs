using App.Core.ViewModels.Dictionaries;
using App.Security.Annotation;

namespace App.Core.Requests.Dictionaries.DictionariesByIds {
    [Access]
    public class GetCurrenciesByIdsRequest : DictionariesByIdsRequest<CurrencyView> {
    }
}
