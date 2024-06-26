using App.Core.ViewModels.Users;

namespace App.Core.Commands.UserProfiles;

public class UpdateUserTgmCommand : IRequest<BaseUserView> {
    public UpdateUserTgmCommand(Guid userId, long? tgmId, string? tgmUsername) {
        UserId = userId;
        TgmId = tgmId;
        TgmUsername = tgmUsername;
    }

    public Guid UserId { get; set; }
    public long? TgmId { get; set; }
    public string? TgmUsername { get; set; }
}