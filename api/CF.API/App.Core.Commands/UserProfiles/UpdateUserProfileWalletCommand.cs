using App.Core.ViewModels.Users;

namespace App.Core.Commands.UserProfiles;

public class UpdateUserProfileWalletCommand : IRequest<UserProfileView> {
    public Guid UbikiriUserId { get; set; }
    public string LinkedWallet { get; set; }
}