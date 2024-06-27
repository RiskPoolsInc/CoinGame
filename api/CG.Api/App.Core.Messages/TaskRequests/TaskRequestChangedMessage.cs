using App.Core.Enums;
using App.Core.Messages.Audits;

using MediatR;

namespace App.Core.Messages.TaskRequests;

public class TaskRequestChangedMessage : AuditBase, INotification, IRequest<NotificationMessage[]> {
    public override AuditEventTypes Type => AuditEventTypes.TaskRequestChanged;
}