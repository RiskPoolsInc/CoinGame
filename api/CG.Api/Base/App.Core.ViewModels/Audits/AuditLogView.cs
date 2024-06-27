using App.Core.ViewModels.Dictionaries;
using App.Core.ViewModels.Security;

namespace App.Core.ViewModels.Audits {

public class AuditLogView : BaseView
{
    public UserBaseView CreatedBy { get; set; }
    public AuditEventTypeView Type { get; set; }
    public string IP { get; set; }

    public Guid? TaskId { get; set; }
    public Guid? OpportunityId { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? AgencyContactId { get; set; }
    public Guid? AgencyId { get; set; }
    public Guid? CampaignId { get; set; }
    public Guid? PolicyId { get; set; }
    public Guid? TeamUserId { get; set; }
    public Guid? ExternalPolicyId { get; set; }
}
}