using App.Core.Enums;
using App.Core.ViewModels.Security;

namespace App.Core.Commands.Users {

public class CreateUserCommand : UserCommand, IRequest<UserView>
{
    public CreateUserCommand(CreateUserInfo adminInfo,
        int mfaTypeId,
        IEnumerable<Guid> roles,
        IEnumerable<Guid> teams,
        IEnumerable<Guid> accesses
    )
    {
        FirstName = adminInfo.FirstName;
        LastName = adminInfo.LastName;
        Email = adminInfo.Email;
        MFATypeId = (int) MultiFactorAuthTypes.None;
    }

    public CreateUserCommand()
    {
    }
}
}