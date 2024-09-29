using App.Core.Enums;
using App.Core.Requests.AutoPaymentServiceRequests;
using App.Core.ViewModels.Dictionaries;
using App.Services.AutoPayment;

using MediatR;

namespace App.Core.Requests.Handlers.AutoPaymentServiceRequests;

public class GetAutoPaymentServiceStatusHandler : IRequestHandler<GetAutoPaymentServiceStatusRequest, AutoPaymentServiceStatusView> {
    private readonly AutoPaymentService _service;

    public GetAutoPaymentServiceStatusHandler(AutoPaymentService service) {
        _service = service;
    }

    public async Task<AutoPaymentServiceStatusView>
        Handle(GetAutoPaymentServiceStatusRequest request, CancellationToken cancellationToken) {
        var status = _service.Status();
        var statusText = ((AutoPaymentServiceStatuses)status).ToString();

        var statusView = new AutoPaymentServiceStatusView {
            Id = (int)status,
            Code = statusText,
            Name = statusText
        };
        return statusView;
    }
}