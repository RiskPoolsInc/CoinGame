using App.Core.ViewModels.CustomerCompanies;
using App.Core.ViewModels.Dictionaries;

namespace App.Core.ViewModels.Users;

public class UserProfileView : BaseUserView {
    public Guid UbikiriUserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserTypeView Type { get; set; }
    public Guid? ReferralId { get; set; }
    public CustomerCompanyBaseView? Company { get; set; }
    public string? BotCheckReasons { get; set; }
    public bool? IsBlocked { get; set; }
    public bool RegistrationComplete { get; set; }
}