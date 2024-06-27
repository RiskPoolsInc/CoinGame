using App.Core.ViewModels.Dictionaries;
using App.Security.Annotation;
using MediatR;

namespace App.Core.Requests.Dictionaries.DictionariesById {
    [Access]
    public class GetCountryStateByIdRequest : IRequest<CountryStateView> {
        public int Id { get; private set; }
        public GetCountryStateByIdRequest(int id) {
            Id = id;
        }
    }
}
