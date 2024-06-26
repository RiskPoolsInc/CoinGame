using App.Core.ViewModels.Users;

namespace App.Core.Commands.UserProfiles;

[AdminAccess]
public class UpdateUserProfileCommand : UserProfileCommand, IRequest<UserProfileView> {
    public Guid Id { get; set; }
}