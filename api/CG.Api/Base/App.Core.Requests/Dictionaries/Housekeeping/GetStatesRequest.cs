using App.Core.ViewModels.Dictionaries;
using App.Security.Annotation;
using MediatR;

namespace App.Core.Requests.Dictionaries.Housekeeping {
    [Access]
    public class GetStatesRequest : IRequest<CountryStateView[]> {
        public string StateName { get; }
        public GetStatesRequest() { } 

        public GetStatesRequest(string stateName = null) {
            StateName = stateName;
        }
    }
}