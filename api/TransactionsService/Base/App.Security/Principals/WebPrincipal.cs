using System.Security.Claims;

using App.Interfaces.Security;

namespace App.Security.Principals;

public class WebPrincipal : ICurrentRequestClient {
    private readonly ClaimsIdentity _identity;

    public WebPrincipal(ClaimsIdentity? identity) {
        _identity = identity ?? throw new ArgumentNullException("identity");
    }

    public bool IsAnonymous => false;
    public Guid? ClientId { get; set; }
}