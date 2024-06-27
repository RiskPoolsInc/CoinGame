using App.Interfaces.Security;

namespace App.Security.Principals;

public class AnonymousPrincipal : ICurrentUser {
    public string? JwtToken => null;
    public bool IsAdmin => false;
    public bool IsCustomer => false;
    public bool IsExecutor => false;
    public bool IsAnonymous => true;
    public bool IsTaskManager => false;

    public Guid UserId => Guid.Empty;
    public Guid UserProfileId => Guid.Empty;
    public Guid? CompanyId => null;
    public bool IsBlocked { get; set; } = false;
    public bool RegistrationComplete => false;
}