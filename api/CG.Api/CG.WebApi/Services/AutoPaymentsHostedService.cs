using App.Services.AutoPayment;

namespace CG.WebApi.Services;

public class AutoPaymentsHostedService : BackgroundService {
    private readonly ILogger<AutoPaymentsHostedService> _logger;
    private readonly AutoPaymentService _autoPaymentService;

    public AutoPaymentsHostedService(ILogger<AutoPaymentsHostedService> logger, AutoPaymentService autoPaymentService) {
        _logger = logger;
        _autoPaymentService = autoPaymentService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        while (!stoppingToken.IsCancellationRequested) {
            try {
                await _autoPaymentService.WorkAsync(stoppingToken);
            }
            catch (Exception e) {
                _logger.LogError(e.ToString());
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}