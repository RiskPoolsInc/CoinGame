using App.Core.ViewModels.Dictionaries;

namespace App.Core.ViewModels.UserLogs;

public class UserLogView : BaseView {
    public Guid? ReferralId { get; set; }
    public Guid? UserId { get; set; }
    public Guid? UbikiriUserId { get; set; }
    public int TypeId { get; set; }
    public UserLogTypeView Type { get; set; }
    public string IP { get; set; }
    public string? MAC { get; set; }
    public string Error { get; set; }
}