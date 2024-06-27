using App.Core.Enums;
using App.Core.Messages.Audits;
using MediatR;

namespace App.Core.Messages.Users;

public class UsersBlockedMessage: AuditBase, IRequest<NotificationMessage[]>, INotification
{
    public Guid[] Ids { get; set; }
    public override AuditEventTypes Type => AuditEventTypes.UserBlocked;
    public string Title { get; set; }
}
    
