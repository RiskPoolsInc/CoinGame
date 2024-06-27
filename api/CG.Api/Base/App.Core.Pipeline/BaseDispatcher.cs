using App.Interfaces.Core;

using MediatR;

namespace App.Core.Pipeline;

public class BaseDispatcher : Mediator, IDispatcher {
    public BaseDispatcher(ServiceFactory serviceFactory) : base(serviceFactory) {
    }
}