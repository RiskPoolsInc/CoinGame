using App.Core.Enums;

namespace App.Core.Commands.Users {

public abstract class UserCommand
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? MFATypeId { get; set; }
    public int? UserTypeId { get; set; }
    public Guid? UbikiriUserId { get; set; }
    public string? TwitterId { get; set; }
    public string? TelegramId { get; set; }

    public MultiFactorAuthTypes? MFAType
    {
        get => (MultiFactorAuthTypes?) MFATypeId;
        set => MFATypeId = (int?) value;
    }

    public bool? IsActive { get; set; }
}
}