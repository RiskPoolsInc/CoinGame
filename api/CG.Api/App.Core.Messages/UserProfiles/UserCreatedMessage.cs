using App.Core.Enums;
using App.Core.Messages.Audits;

using MediatR;

namespace App.Core.Messages.UserProfiles;

public class UserCreatedMessage : AuditBase, INotification, IRequest<NotificationMessage[]> {
    public override AuditEventTypes Type => AuditEventTypes.UserCreated;
}