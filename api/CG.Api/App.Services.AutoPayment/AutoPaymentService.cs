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
    private AutoPaymentServiceStatuses _status = AutoPaymentServiceStatuses.Stop;

    public AutoPaymentService(ILogger<AutoPaymentService> logger,     IAutoPaymentServiceOptions options,
                              IDispatcher                 dispatcher, ISendRewardsHandler        handler) {
        _logger = logger;
        _options = options;
        _dispatcher = dispatcher;
        _handler = handler;
    }

    public async Task WorkAsync(CancellationToken cancellationToken) {
        _logger.LogInformation("Auto Payment Service updating status to Database");
        await _dispatcher.Send(new CreateAutoPaymentServiceLogCommand(AutoPaymentServiceStatuses.Run));
        _logger.LogInformation("Auto Payment Service Runned");
        var cycleTime = _options.TimeRepeatAutoPaymentMilliseconds;
        _status = AutoPaymentServiceStatuses.Run;

        try {
            do {
                if (_status == AutoPaymentServiceStatuses.Stop) {
                    await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
                    continue;
                }

                _logger.LogInformation("Auto Payment Service Send Rewards Command");
                var result = await _handler.Handle(new SendRewardsCommand(), cancellationToken);
                _logger.LogInformation("Delay {time} ms at: {currentTime}", cycleTime, DateTimeOffset.Now);
                await Task.Delay(cycleTime, cancellationToken);
                _logger.LogInformation("Delay {time} ms completed at: {currentTime}", cycleTime, DateTimeOffset.Now);

                if (_status == AutoPaymentServiceStatuses.Stop)
                    _logger.LogInformation("Auto Payment Service is paused");
            } while (!cancellationToken.IsCancellationRequested);

            _logger.LogInformation("Auto Payment Service stopping");
            _logger.LogInformation("Auto Payment Service updating status to Database");
            await _dispatcher.Send(new CreateAutoPaymentServiceLogCommand(AutoPaymentServiceStatuses.Stop));
            _logger.LogInformation("Auto Payment Service stopped");
        }
        catch (Exception e) {
            _logger.LogInformation($"Auto Payment Service stopped with fault: {e}");
            _status = AutoPaymentServiceStatuses.Stop;
            throw;
        }
    }

    public AutoPaymentServiceStatuses Status() {
        return _status;
    }
}