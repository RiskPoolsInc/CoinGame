using App.Interfaces.Security;

namespace App.Security.Principals;

public class AnonymousPrincipal : ICurrentRequestClient {
    public bool IsAnonymous => true;

    public Guid? ProfileId { get; set; }
}