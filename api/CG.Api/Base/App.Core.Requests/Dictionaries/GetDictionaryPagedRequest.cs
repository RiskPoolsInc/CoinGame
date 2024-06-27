using App.Core.Requests.Interfaces;
using App.Core.ViewModels;
using App.Data.Criterias.Core;
using App.Data.Entities;
using App.Interfaces.RequestsParams;
using MediatR;
using IPagedRequest = App.Core.Requests.Interfaces.IPagedRequest;

namespace App.Core.Requests.Dictionaries {
    public abstract class GetDictionaryPagedRequest<T> : GetDictionaryPagedRequest, IRequest<PagedList<T>> {

    }

    public abstract class GetDictionaryPagedRequest : IPagedRequest {
        public DictionaryFilter Filter { get; set; }
        public KeyWordFilter[] Keywords { get; set; }
        public int[] Values { get; set; }
        public bool TwoFactorFiltering { get; set; }
        public int[] ValuesOnTop { get; set; }
        public string[] ManualsOnTop { get; set; }
        public int? Page { get; set; }
        public int? Size { get; set; }
        public string Sort { get; set; }
        public int Direction { get; set; }
        public bool? GetAll { get; set; }
        public bool? WithDeleted { get; set; }
        public int? Skip { get; set; }
    }
}