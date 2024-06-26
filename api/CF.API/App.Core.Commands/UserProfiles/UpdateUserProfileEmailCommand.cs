using App.Core.ViewModels.Users;

namespace App.Core.Commands.UserProfiles;

public class UpdateUserProfileEmailCommand : IRequest<UserProfileView> {
    public UpdateUserProfileEmailCommand(string email) : this() {
        Email = email;
    }

    public UpdateUserProfileEmailCommand() {
    }

    public string Email { get; set; }
}