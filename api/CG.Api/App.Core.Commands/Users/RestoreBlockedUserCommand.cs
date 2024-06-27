using App.Security.Annotation;

namespace App.Core.Commands.Users;

[CustomerAccess]
public class RestoreBlockedUserCommand : IRequest
{
    public Guid UserId { get; set; }

    public RestoreBlockedUserCommand(Guid userId) : this()
    {
        UserId = userId;
    }

    public RestoreBlockedUserCommand()
    {
    }
}