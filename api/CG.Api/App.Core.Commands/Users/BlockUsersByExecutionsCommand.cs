using App.Security.Annotation;

namespace App.Core.Commands.Users;

[CustomerAccess]
public class BlockUsersByExecutionsCommand: IRequest
{
    public Guid[] TaskExecutionIds { get; set; }
}