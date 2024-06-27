using App.Core.Messages.Audits;
using MediatR;

namespace App.Core.Messages;

public abstract class BaseNotificationMessage : AuditBase, INotification
{
    public Guid Id { get; set; }
    public string Title { get; set; }
}