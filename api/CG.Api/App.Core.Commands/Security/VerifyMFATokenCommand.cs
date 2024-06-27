namespace App.Core.Commands.Security {

public class VerifyMFATokenCommand : IRequest<bool>
{
    public string Token { get; set; }
}
}