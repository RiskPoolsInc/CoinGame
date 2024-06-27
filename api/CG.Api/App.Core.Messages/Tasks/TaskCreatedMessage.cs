using App.Core.Enums;
using App.Core.Messages.Audits;

using MediatR;

namespace App.Core.Messages.Tasks;

public class TaskCreatedMessage : BaseNotificationMessage, IRequest<NotificationMessage[]> {
    public override AuditEventTypes Type => AuditEventTypes.TaskCreated;
}