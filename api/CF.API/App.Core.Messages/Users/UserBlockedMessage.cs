using App.Core.Enums;
using App.Core.Messages.Audits;
using MediatR;

namespace App.Core.Messages.Users;

public class UserBlockedMessage: AuditBase, IRequest<NotificationMessage[]>, INotification
{
    public Guid Id { get; set; }
    public override AuditEventTypes Type => AuditEventTypes.UserBlocked;
    public string Title { get; set; }
}
    
