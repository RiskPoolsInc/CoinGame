using App.Interfaces.Security;

namespace App.Security.Principals;

public class AnonymousPrincipal : ICurrentRequestClient {
    public bool IsAnonymous => true;

    public Guid? ClientId { get; set; }
    public bool IsTaskManager => false;
}