using App.Core.ViewModels.Dictionaries;

namespace App.Core.ViewModels.Users;

public class UserProfileView : BaseUserView {
    public Guid UbikiriUserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserTypeView Type { get; set; }
}