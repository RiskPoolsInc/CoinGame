using App.Core.ViewModels.Dictionaries;

namespace App.Core.ViewModels.Attachments;

public class BaseAttachmentView : BaseView {
    public ObjectTypeView ObjectType { get; set; }
    public string Description { get; set; }
    public Guid ObjectId { get; set; }
    public string FileName { get; set; }
}