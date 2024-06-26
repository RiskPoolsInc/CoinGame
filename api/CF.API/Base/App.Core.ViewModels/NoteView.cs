using App.Core.ViewModels.Security;

namespace App.Core.ViewModels {

public class NoteView : BaseView
{
    public UserBaseView CreatedBy { get; set; }
    public string Body { get; set; }
}
}