using App.Core.ViewModels.Security;

namespace App.Core.Commands.Security {

public class EditTenantUserCommand : IRequest<UserView>
{
    public UpdateTenantUserCommand BasicInfo { get; set; }
    public Guid Id { get; set; }
}
}