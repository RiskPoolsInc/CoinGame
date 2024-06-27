using App.Core.Enums;
using App.Core.ViewModels.Security;

namespace App.Core.Messages.Audits;

public abstract class AuditBase {
    public DateTime CreatedOn { get; set; }
    public UserBaseView CreatedBy { get; set; }
    public string IP { get; set; }
    public abstract AuditEventTypes Type { get; }
}