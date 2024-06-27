using App.Core.ViewModels.Security;

namespace App.Core.Commands.Users {

[Access]
public class ActivateUserCommand : IRequest<UserView>
{
    public string Code { get; set; }
    public string Password { get; set; }
}
}