namespace App.Core.Commands.ExternalSystems;

public class SendPostRequestCommand : IRequest<object> {
    public string EndpointPath { get; set; }
    public object RequestValue { get; set; } = null!;
    public Type ResponseType { get; set; } = null!;
}