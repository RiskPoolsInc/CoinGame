using App.Core.Enums;
using App.Core.Messages.Audits;

using MediatR;

namespace App.Core.Messages.UserProfiles;

public class UserChangedMessage : AuditBase, INotification, IRequest<NotificationMessage[]> {
    public override AuditEventTypes Type => AuditEventTypes.UserChanged;
}