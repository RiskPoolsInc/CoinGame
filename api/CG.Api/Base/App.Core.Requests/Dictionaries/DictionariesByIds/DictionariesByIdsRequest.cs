using App.Security.Annotation;
using MediatR;

namespace App.Core.Requests.Dictionaries.DictionariesByIds {
    [Access]
    public class DictionariesByIdsRequest<TView> : IRequest<TView[]> where TView : DictionaryBaseView {
        public int[] Ids { get; set; }
        public bool ExcludeMode { get; set; }
    }
}
