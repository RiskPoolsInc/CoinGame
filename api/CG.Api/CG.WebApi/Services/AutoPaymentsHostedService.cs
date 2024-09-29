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
        await _autoPaymentService.WorkAsync(stoppingToken);
    }
}