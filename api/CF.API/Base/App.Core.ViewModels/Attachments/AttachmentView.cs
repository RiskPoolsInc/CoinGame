using App.Core.ViewModels.CustomerCompanies;
using App.Core.ViewModels.Security;

namespace App.Core.ViewModels.Attachments;

public class AttachmentView : BaseAttachmentView {
    public CustomerCompanyBaseView CreatedBy { get; set; }
    public UserBaseView CreatedByUser { get; set; }
}