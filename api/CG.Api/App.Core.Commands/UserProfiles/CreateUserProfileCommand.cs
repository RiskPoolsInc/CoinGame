using App.Core.ViewModels.Users;

namespace App.Core.Commands.UserProfiles;

public class CreateUserProfileCommand : UserProfileCommand, IRequest<UserProfileView> {
    public Guid UbikiriUserId { get; set; }
    public override string TelegramId { get; set; }
    public override string TwitterId { get; set; }

    /// <summary>
    ///     hCaptcha generated token
    /// </summary>
    public string CaptchaToken { get; set; }
}