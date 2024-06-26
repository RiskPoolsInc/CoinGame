namespace App.Core.Commands.Users {

[Access]
public class ChangeMyPasswordCommand : IRequest<bool>
{
    public string OldPassword { get; set; }
    public string Password { get; set; }
}
}