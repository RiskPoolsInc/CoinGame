using App.Core.Enums;

namespace App.Core.ViewModels;

public class ExternalFile : BaseView {
    public string DisplayName { get; set; }
    public string FileName { get; set; }
    public ObjectTypes ObjectType { get; set; }
}