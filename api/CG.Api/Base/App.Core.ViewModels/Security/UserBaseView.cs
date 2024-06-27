using App.Core.ViewModels.Dictionaries;

namespace App.Core.ViewModels.Security;

public class UserBaseView : BaseView
{
    public UserTypeView Type { get; set; }
    public bool IsActive { get; set; }
    public bool IsBlocked { get; set; }
    public bool RegistrationComplete { get; set; }
}