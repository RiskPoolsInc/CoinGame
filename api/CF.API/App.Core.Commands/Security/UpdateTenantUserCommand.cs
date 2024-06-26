using App.Core.ViewModels.Security;

namespace App.Core.Commands.Security {

public class UpdateTenantUserCommand : IRequest<UserView>
{
    public Guid Id { get; set; }
    public Guid? OfficeId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public bool? IsActive { get; set; }
}
}