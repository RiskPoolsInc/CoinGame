using App.Core.Commands.AutoPaymentServiceCommands;
using App.Core.Commands.Transactions;
using App.Core.Enums;
using App.Interfaces.Core;
using App.Interfaces.Handlers;

using Microsoft.Extensions.Logging;

namespace App.Services.AutoPayment;

public class AutoPaymentService : IAutoPaymentService {
    private readonly ILogger<AutoPaymentService> _logger;
    private readonly IAutoPaymentServiceOptions _options;
    private readonly IDispatcher _dispatcher;
    private readonly ISendRewardsHandler _handler;
    private CancellationTokenSource _cts = new();
    private int _status = (int)AutoPaymentServiceStatuses.Stop;

    public AutoPaymentService(ILogger<AutoPaymentService> logger,     IAutoPaymentServiceOptions options,
                              IDispatcher                 dispatcher, ISendRewardsHandler        handler) {
        _logger = logger;
        _options = options;
        _dispatcher = dispatcher;
        _handler = handler;
    }

    public async Task RunAsync(CancellationToken cancellationToken) {
        _logger.LogInformation("Starting Auto Payment Service");

        if ((AutoPaymentServiceStatuses)_status == AutoPaymentServiceStatuses.Run)
            throw new Exception("AutoPayment service is already running.");

        _cts = new CancellationTokenSource();
        await WorkAsync(cancellationToken);
    }

    public void Stop() {
        _logger.LogInformation("Stopping Auto Payment Service");

        if (!_cts.IsCancellationRequested)
            _cts.Cancel();
        else
            throw new Exception("AutoPayment service is already stopped.");
    }

    public async Task WorkAsync(CancellationToken cancellationToken) {
        _logger.LogInformation("Auto Payment Service updating status to Database");
        await _dispatcher.Send(new CreateAutoPaymentServiceLogCommand(AutoPaymentServiceStatuses.Run));
        _logger.LogInformation("Auto Payment Service Runned");
        var cycleTime = _options.TimeRepeatAutoPaymentMilliseconds;

        do {
            _logger.LogInformation("Auto Payment Service Send Rewards Command");
            await _handler.Handle(new SendRewardsCommand(), cancellationToken);
            _logger.LogInformation("Delay {time} ms...", cycleTime);
            await Task.Delay(cycleTime, cancellationToken);
            _logger.LogInformation("Delay {time} ms completed", cycleTime);
        } while (!cancellationToken.IsCancellationRequested);

        _logger.LogInformation("Auto Payment Service stopping");
        _logger.LogInformation("Auto Payment Service updating status to Database");
        await _dispatcher.Send(new CreateAutoPaymentServiceLogCommand(AutoPaymentServiceStatuses.Stop));
        _logger.LogInformation("Auto Payment Service stopped");
    }

    public int Status() {
        return _status;
    }
}