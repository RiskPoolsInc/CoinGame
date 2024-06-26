using App.Core.ViewModels.Users;

namespace App.Core.Commands.UserProfiles;

public class UpdateUserTgmCodeCommand : IRequest<BaseUserView> {
    public UpdateUserTgmCodeCommand(Guid userId, Guid verificationCodeId) {
        UserId = userId;
        VerificationCodeId = verificationCodeId;
    }

    public UpdateUserTgmCodeCommand() {
    }

    public Guid UserId { get; set; }
    public Guid VerificationCodeId { get; set; }
}