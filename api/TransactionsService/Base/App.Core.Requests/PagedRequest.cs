using IPagedRequest = App.Core.Requests.Interfaces.IPagedRequest;

namespace App.Core.Requests {
    public abstract class PagedRequest<TResult> : IRequest<IPagedList<TResult>>, 
        IPagedRequest {
        public string? Sort { get; set; }
        public int Direction { get; set; }

        public int? Page { get; set; }
        public int? Skip { get; set; }
        public int? Size { get; set; }
        public bool? GetAll { get; set; }
        public bool? WithDeleted { get; set; }
    }
}
