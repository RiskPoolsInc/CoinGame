namespace App.Core.Commands.ExternalSystems;

public class SendPostRequestToCustomerCommand : IRequest<object> {
    public Guid CustomerId { get; set; }
    public int EndpointTypeId { get; set; }
    public object RequestValue { get; set; } = null!;
    public Type ResponseType { get; set; } = null!;
}