using App.Core.ViewModels.Dictionaries;
using App.Security.Annotation;
using MediatR;

namespace App.Core.Requests.Dictionaries.DictionariesById {
    [Access]
    public class GetCountryByIdRequest : IRequest<CountryView> {
        public GetCountryByIdRequest(int id) {
            Id = id;
        }
        public int Id { get; private set; }
    }
}
