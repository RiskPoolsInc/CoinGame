using App.Core.ViewModels.Users;

namespace App.Core.Commands.UserProfiles;

public class CheckUserTgmCodeCommand : IRequest<BaseUserView> {
    public string Code { get; set; }
}