using App.Security.Annotation;

namespace App.Core.Commands.Users {

[Access]
public class UserForgotPasswordCommand : IRequest<bool>
{
    public string Email { get; set; }
}
}