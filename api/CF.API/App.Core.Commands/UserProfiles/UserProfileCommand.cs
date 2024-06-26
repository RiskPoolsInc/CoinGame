namespace App.Core.Commands.UserProfiles;

public abstract class UserProfileCommand {
    public Guid? ReferralId { get; set; }
    public int? ReferralTypeId { get; set; }
    public int? TypeId { get; set; }
    public Guid? CompanyId { get; set; }
    public string? LinkedWallet { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public virtual string? TelegramId { get; set; }
    public virtual string? TwitterId { get; set; }
    public bool RegistrationComplete { get; set; }
}