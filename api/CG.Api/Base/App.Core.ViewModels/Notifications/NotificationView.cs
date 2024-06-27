using App.Core.ViewModels.Security;

namespace App.Core.ViewModels.Notifications; 

public class NotificationView : BaseView {
    public int TypeId { get; set; }
    public Guid ObjectId { get; set; }
    public string JsonObjectView { get; set; }
    public Guid? OpportunityId { get; set; }
    public Guid? ParentId { get; set; }
    public string Title { get; set; }
    public UserBaseView CreatedBy { get; set; }
    public Guid RecipientId { get; set; }
}