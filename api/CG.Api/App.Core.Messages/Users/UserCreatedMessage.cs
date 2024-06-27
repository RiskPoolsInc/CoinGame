using App.Core.Enums;
using MediatR;

namespace App.Core.Messages.Users {

public class UserCreatedMessage : UserBaseMessage, IRequest<NotificationMessage[]>
{
    public Guid? OfficeId { get; set; }
    public override AuditEventTypes Type => AuditEventTypes.UserCreated;
}
}