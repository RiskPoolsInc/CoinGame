using App.Core.ViewModels.Security;

namespace App.Core.Commands.Users {

public class UpdateUserBasicInfoCommand : UserCommand, IRequest<UserView>
{
    public Guid Id { get; set; }
}
}