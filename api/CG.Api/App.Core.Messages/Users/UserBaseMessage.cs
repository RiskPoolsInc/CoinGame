using App.Core.Messages.Audits;
using MediatR;

namespace App.Core.Messages.Users {

public abstract class UserBaseMessage : AuditBase, INotification
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
}
}