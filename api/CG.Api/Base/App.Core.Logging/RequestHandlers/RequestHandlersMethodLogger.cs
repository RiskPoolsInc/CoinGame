using Microsoft.Extensions.Logging;

namespace App.Core.Logging.RequestHandlers;

public class RequestHandlersMethodLogger : BaseMethodLogger<RequestHandlersMethodLogger> {
    public RequestHandlersMethodLogger(ILogger<RequestHandlersMethodLogger> logger) : base(logger) {
        Filter = invocation => invocation.Method.Name != "Handle" && invocation.Method.Name != "HandleAsync";
    }
}