using App.Core.ViewModels.Security;

namespace App.Core.ViewModels.Attachments;

public class AttachmentView : BaseAttachmentView {
    public UserBaseView CreatedByUser { get; set; }
}