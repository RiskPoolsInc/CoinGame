using App.Core.ViewModels.Dictionaries;
using App.Security.Annotation;
using MediatR;

namespace App.Core.Requests.Dictionaries.DictionariesById {
    [Access]
    public class GetCurrencyByIdRequest : IRequest<CurrencyView> {
        public int Id { get; private set; }
        public GetCurrencyByIdRequest(int id) {
            Id = id;
        }
    }
}
