using App.Core.Requests.Interfaces;
using App.Core.ViewModels;
using MediatR;

using IScrolledRequest = App.Interfaces.Core.Requests.IScrolledRequest;

namespace App.Core.Requests {
    public class ScrolledRequest<T> : IRequest<ScrolledList<T>>, IScrolledRequest {
        public string Sort { get; set; }
        public int Direction { get; set; }
        public string Token { get; set; }
        public int? Size { get; set; }
        public int? Skip { get; set; }
    }
}
