using Microsoft.Extensions.Logging;

namespace App.Core.Logging.CommandsHandlers;

public class CommandHandlersMethodLogger : BaseMethodLogger<CommandHandlersMethodLogger> {
    public CommandHandlersMethodLogger(ILogger<CommandHandlersMethodLogger> logger) : base(logger) {
        Filter = invocation => invocation.Method.Name != "Handle";
    }
}