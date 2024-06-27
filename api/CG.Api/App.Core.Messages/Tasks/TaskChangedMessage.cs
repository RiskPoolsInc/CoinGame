using App.Core.Enums;
using App.Core.Messages.Audits;

using MediatR;

namespace App.Core.Messages.Tasks;

public class TaskChangedMessage : BaseNotificationMessage, IRequest<NotificationMessage[]> {
    public override AuditEventTypes Type => AuditEventTypes.TaskChanged;
}