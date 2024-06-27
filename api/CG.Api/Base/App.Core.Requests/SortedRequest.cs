using App.Core.Requests.Interfaces;
using MediatR;

using ISortedRequest = App.Interfaces.Core.Requests.ISortedRequest;

namespace App.Core.Requests {
    public abstract class SortedRequest<TResult> : IRequest<TResult>, ISortedRequest {
        public string Sort { get; set; }
        public int Direction { get; set; }
    }
}
