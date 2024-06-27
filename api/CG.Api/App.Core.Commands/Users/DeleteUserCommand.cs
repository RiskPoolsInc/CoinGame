using App.Security.Annotation;

namespace App.Core.Commands.Users {

[Access]
public class DeleteUserCommand : IRequest<bool>
{
    public DeleteUserCommand() : this(null)
    {
    }

    public DeleteUserCommand(Guid? userId = null)
    {
        if (userId.HasValue)
            UserId = userId.Value;
    }

    public Guid UserId { get; set; }
}
}