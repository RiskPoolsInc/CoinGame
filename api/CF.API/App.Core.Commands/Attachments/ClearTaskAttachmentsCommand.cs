namespace App.Core.Commands.Attachments;

public class ClearTaskAttachmentsCommand : IRequest<bool> {
    public Guid Id { get; set; }
}