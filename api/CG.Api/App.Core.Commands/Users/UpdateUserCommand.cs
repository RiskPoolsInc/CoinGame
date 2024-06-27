using App.Core.ViewModels.Security;

namespace App.Core.Commands.Users {

[AdminAccess]
public class UpdateUserCommand : IRequest<UserView>
{
    public Guid Id { get; set; }
    public UpdateUserBasicInfoCommand BasicInfo { get; set; }
}
}