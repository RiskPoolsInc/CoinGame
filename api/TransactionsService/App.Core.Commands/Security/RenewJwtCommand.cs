using App.Core.ViewModels.Security;

namespace App.Core.Commands.Security {

public class RenewJwtCommand : IRequest<TokenResponse>
{
    public string Token { get; set; }
}
}