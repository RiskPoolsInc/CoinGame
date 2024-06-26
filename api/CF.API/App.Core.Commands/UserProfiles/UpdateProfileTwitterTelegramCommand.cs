using App.Core.ViewModels.Users;

namespace App.Core.Commands.UserProfiles;

public class UpdateProfileTwitterTelegramCommand : IRequest<UserProfileView> {
    public string? TwitterId { get; set; }
    public string? TelegramId { get; set; }
}