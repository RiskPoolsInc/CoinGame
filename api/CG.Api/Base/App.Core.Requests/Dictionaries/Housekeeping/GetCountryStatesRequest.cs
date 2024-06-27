using App.Core.ViewModels.Dictionaries;
using App.Security.Annotation;
using MediatR;

namespace App.Core.Requests.Dictionaries.Housekeeping {
    [Access]
    public class GetCountryStatesRequest : GetStatesRequest, IRequest<CountryStateView[]> {

        public int CountryId { get; }

        public GetCountryStatesRequest(int countryId, string stateName = null) : base(stateName) {
            CountryId = countryId;
        }
    }
}